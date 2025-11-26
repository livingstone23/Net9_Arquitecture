using Pacagroup.Ecommerce.Domain.Entity;



namespace Pacagroup.Ecommerce.Domain.Interface;



/// <summary>
/// Interface para la gestión de clientes
/// </summary>
public interface ICustomersDomain
{
    Task<bool> InsertAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(string customerId);
    Task<Customer> GetAsync(string customerId);
    Task<IEnumerable<Customer>> GetAllAsync();


    Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();

}