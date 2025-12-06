using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.Interface;
using Swashbuckle.AspNetCore.Annotations;



namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2;



//[Authorize]
[ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[SwaggerTag("Catalogo de Categorias")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesApplication _categoriesApplication;
    public CategoriesController(ICategoriesApplication categoriesApplication)
    {
        _categoriesApplication = categoriesApplication;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _categoriesApplication.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response.Message);
    }
}