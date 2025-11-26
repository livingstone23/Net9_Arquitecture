


namespace Pacagroup.Ecommerce.Infrastructure.Interface;



/// <summary>
/// Interfaz genérica para el repositorio
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T : class
{

    Task<bool> InsertAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(string customerId);
    Task<T?> GetAsync(string customerId);
    Task<IEnumerable<T>> GetAllAsync();


    //Metodos de la paginacion
    Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();

}