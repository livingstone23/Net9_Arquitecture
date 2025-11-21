using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Tranversal.Common;



namespace Pacagroup.Ecommerce.Application.Interface;



/// <summary>
/// Interfaz para la aplicación de autenticación
/// </summary>
public interface IAuthApplication
{
    Task<Response<bool>> SignUpAsync(SignUpDto signUpDto);
    Task<Response<TokenDto>> SignInAsync(SignInDto signInDto);
}