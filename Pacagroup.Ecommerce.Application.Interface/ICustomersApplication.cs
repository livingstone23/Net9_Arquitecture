using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Tranversal.Common;



namespace Pacagroup.Ecommerce.Application.Interface;



/// <summary>
/// Interfaz para la aplicación de clientes
/// Permite aplicar el principio de segregación de interfaces
/// </summary>
public interface ICustomersApplication
{

    Task<Response<bool>> InsertAsync(CustomerDto customersDto);
    Task<Response<bool>> UpdateAsync(CustomerDto customersDto);
    Task<Response<bool>> DeleteAsync(string customerId);
    Task<Response<CustomerDto>> GetAsync(string customerId);
    Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();

    Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);

}