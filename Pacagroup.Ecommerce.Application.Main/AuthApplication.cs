using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Tranversal.Common;
using Pacagroup.Ecommerce.Tranversal.Logging;


namespace Pacagroup.Ecommerce.Application.Main;



/// <summary>
/// Clase que implementa los métodos de la aplicación para la autenticación
/// </summary>
public class AuthApplication : IAuthApplication
{

    private readonly IUsersDomain _usersDomain;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<AuthApplication> _logger;

    public AuthApplication(IUsersDomain usersDomain, IJwtService jwtService, IMapper mapper, IAppLogger<AuthApplication> logger)
    {
        _usersDomain = usersDomain;
        _jwtService = jwtService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<TokenDto>> SignInAsync(SignInDto signInDto)
    {
        var response = new Response<TokenDto>();

        try
        {
            var user = await _usersDomain.GetByEmailAsync(signInDto.Email);
            if (user == null)
            {
                response.Message = "Email no existe o no se encuentra registrado";
                _logger.LogError("Failed to validate email. Error: {message}", response.Message);
                return response;
            }

            var isValidPassword = await _usersDomain.CheckPasswordAsync(user, signInDto.Password);
            if (!isValidPassword)
            {
                response.Message = "Credenciales inválidas";
                return response;
            }

            var token = _jwtService.GenerateToken(user);
            response.Data = new TokenDto
            {
                AccessToken = token,
                ExpiresIn = 3600
            };

            response.IsSuccess = true;
            response.Message = "Autenticación exitosa";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> SignUpAsync(SignUpDto signUpDto)
    {
        var response = new Response<bool>();
        try
        {
            var existingUser = await _usersDomain.GetByEmailAsync(signUpDto.Email);
            if (existingUser != null)
            {
                response.Message = "El usuario ya existe";
                return response;
            }

            var user = _mapper.Map<User>(signUpDto);
            response.Data = await _usersDomain.CreateUserAsync(user, signUpDto.Password);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Usuario creado exitosamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

}