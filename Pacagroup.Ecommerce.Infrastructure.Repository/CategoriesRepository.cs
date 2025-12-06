using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase del repositorio de categorías
/// </summary>
public class CategoriesRepository: ICategoriesRepository
{

    private readonly DapperContext _context;

    public CategoriesRepository(DapperContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Método para obtener todas las categorías
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Categories>> GetAll()
    {
        
        using var connection = _context.CreateConnection();

        var query = "Select * from Categories";

        var categories = await connection.QueryAsync<Categories>(query, commandType: CommandType.Text);

        return categories;

    }


}