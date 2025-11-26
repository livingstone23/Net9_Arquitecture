using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Tranversal.Common;



namespace Pacagroup.Ecommerce.Application.Main;



/// <summary>
/// Clase que implementa los métodos de la aplicación para los clientes
/// </summary>
public class CustomersApplication : ICustomersApplication
{

    private readonly ICustomersDomain _customersDomain;
    private readonly IMapper _mapper;

    public CustomersApplication(ICustomersDomain customersDomain, IMapper mapper)
    {
        _customersDomain = customersDomain;
        _mapper = mapper;
    }

    public async Task<Response<bool>> InsertAsync(CustomerDto customersDto)
    {

        var response = new Response<bool>();
        try
        {
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = await _customersDomain.InsertAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;

    }

    public async Task<Response<bool>> UpdateAsync(CustomerDto customersDto)
    {

        var response = new Response<bool>();
        try
        {
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = await _customersDomain.UpdateAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa!!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = $"Cliente {customersDto.CustomerId} no existe!!!";
            }

        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;

    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {

        var response = new Response<bool>();
        try
        {
            response.Data = await _customersDomain.DeleteAsync(customerId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;

    }

    public async Task<Response<CustomerDto>> GetAsync(string customerId)
    {

        var response = new Response<CustomerDto>();
        try
        {
            var customer = await _customersDomain.GetAsync(customerId);
            response.Data = _mapper.Map<CustomerDto>(customer);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = $"Cliente {customerId} no existe!!!";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;

    }

    public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CustomerDto>>();
        try
        {
            var customers = await _customersDomain.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDto>>();
        try
        {
            var count = await _customersDomain.CountAsync();

            var customers = await _customersDomain.GetAllWithPaginationAsync(pageNumber, pageSize);
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Consulta Paginada Exitosa!!!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return response;
    }
}