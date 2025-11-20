using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Tranversal.Common;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;



namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;



[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operaciones relacionadas con Clientes")]
public class CustomersController : ControllerBase
{
    

    private readonly ICustomersApplication _customersApplication;
        
    public CustomersController(ICustomersApplication customersApplication)
    {
        _customersApplication = customersApplication;
    }
    

    
    [HttpPost("InsertAsync")]
    [SwaggerOperation(Summary = "Registra un Cliente", Description = "Retorna un objeto generico con el resultado de la operación")]
    [SwaggerResponse(200, "Cliente registrado", typeof(Response<bool>))]
    public async Task<IActionResult> InsertAsync([FromBody] CustomerDto customerDto)
    {

        if (customerDto == null)
            return BadRequest();

        var response = await _customersApplication.InsertAsync(customerDto);

        if (response.IsSuccess)
            return Ok(response);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);

    }



    [HttpPut("UpdateAsync/{customerId}")]
    [SwaggerOperation(
        Summary = "Actualiza un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
    [SwaggerResponse(200, "Cliente actualizado", typeof(Response<bool>))]
    public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody] CustomerDto customerDto)
    {
        if (customerDto == null)
            return BadRequest();

        if (!customerId.Equals(customerDto.CustomerId))
            return BadRequest();

        var response = await _customersApplication.UpdateAsync(customerDto);

        if (response.IsSuccess)
            return Ok(response);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);
    }



    [HttpDelete("DeleteAsync/{customerId}")]
    [SwaggerOperation(
        Summary = "Elimina un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
    [SwaggerResponse(200, "Cliente eliminado", typeof(Response<bool>))]
    public async Task<IActionResult> DeleteAsync([FromRoute] string customerId)
    {

        if (string.IsNullOrEmpty(customerId))
            return BadRequest();

        var response = await _customersApplication.DeleteAsync(customerId);

        if (response.IsSuccess)
            return Ok(response);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);

    }



    [HttpGet("GetAsync/{customerId}")]
    [SwaggerOperation(
        Summary = "Obtiene un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
    [SwaggerResponse(200, "Cliente encontrado", typeof(Response<CustomerDto>))]
    public async Task<IActionResult> GetAsync([FromRoute] string customerId)
    {

        if (string.IsNullOrEmpty(customerId))
            return BadRequest();

        var response = await _customersApplication.GetAsync(customerId);

        if (response.IsSuccess)
            return Ok(response);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);

    }



    [HttpGet("GetAllAsync")]
    [SwaggerOperation(
        Summary = "Lista la totalidad de Clientes",
        Description = "Retorna un objeto generico con el resultado de la operación")]
    [SwaggerResponse(200, "Clientes encontrados", typeof(Response<IEnumerable<CustomerDto>>))]
    public async Task<IActionResult> GetAllAsync()
    {

        var response = await _customersApplication.GetAllAsync();

        if (response.IsSuccess)
            return Ok(response);

        return StatusCode((int)HttpStatusCode.InternalServerError, response);

    }



}

