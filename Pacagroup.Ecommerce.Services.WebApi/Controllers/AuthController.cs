using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Swashbuckle.AspNetCore.Annotations;



namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;


//[Authorize]
[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operaciones de Autenticación")]
public class AuthController : ControllerBase
{

    private readonly IAuthApplication _authApplication;

    public AuthController(IAuthApplication authApplication)
    {
        _authApplication = authApplication;
    }

    //[AllowAnonymous]
    [HttpPost("SignUp")]
    [SwaggerOperation(Summary = "Registra un nuevo usuario")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUpDto)
    {
        var response = await _authApplication.SignUpAsync(signUpDto);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    //[AllowAnonymous]
    [HttpPost("SignIn")]
    [SwaggerOperation(Summary = "Autentica un usuario y genera token")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInDto signInDto)
    {
        var response = await _authApplication.SignInAsync(signInDto);

        if (response.IsSuccess)
            return Ok(response);

        return Unauthorized(response);

    }

}

