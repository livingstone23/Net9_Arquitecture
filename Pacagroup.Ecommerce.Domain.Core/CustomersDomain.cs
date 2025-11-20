#region MyRegion

using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

#endregion



namespace Pacagroup.Ecommerce.Domain.Core;



/// <summary>
/// Clase que implementa la lógica de negocio para los clientes
/// </summary>
public class CustomersDomain : ICustomersDomain
{


    /// <summary>
    /// Propiedad para implementar el patrón Unit of Work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    public CustomersDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        return await _unitOfWork.Customers.DeleteAsync(customerId);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _unitOfWork.Customers.GetAllAsync();
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        return await _unitOfWork.Customers.GetAsync(customerId);
    }

    public async Task<bool> InsertAsync(Customer customer)
    {
        return await _unitOfWork.Customers.InsertAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        return await _unitOfWork.Customers.UpdateAsync(customer);
    }


}