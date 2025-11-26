using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;
using Dapper;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase que implementa el repositorio de clientes
/// </summary>
public class CustomersRepository : ICustomersRepository
{

    private readonly DapperContext _context;

    public CustomersRepository(DapperContext context)
    {
        _context = context;
    }



    /// <summary>
    /// Metodo para obtener todos los clientes
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {

        using var connection = _context.CreateConnection();
        var query = "CustomersList";

        var customers = await connection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);

        return customers;

    }



    /// <summary>
    /// Metodo para obtener un cliente por su ID
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public async Task<Customer?> GetAsync(string customerId)
    {

        using var connection = _context.CreateConnection();
        var query = "CustomersGetByID";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customerId);

        /// Si no encuentra el cliente, devuelve null
        var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);

        return customer;

    }



    /// <summary>
    /// Metodo para insertar un nuevo cliente
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public async Task<bool> InsertAsync(Customer customer)
    {

        using var connection = _context.CreateConnection();

        // Definimos el nombre del procedimiento almacenado
        var query = "CustomersInsert";


        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);

        var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

        return result > 0;

    }



    /// <summary>
    /// Metodo para actualizar un cliente
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(Customer customer)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersUpdate";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);

        var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

        return result > 0;
    }



    /// <summary>
    /// Metodo para eliminar un cliente
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(string customerId)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersDelete";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customerId);

        var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

        return result > 0;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersListWithPagination";
        var parameters = new DynamicParameters();
        parameters.Add("PageNumber", pageNumber);
        parameters.Add("PageSize", pageSize);

        var customers = await connection.QueryAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
        return customers;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> CountAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "Select Count(*) from Customers";

        var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
        return count;
    }


}