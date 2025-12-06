using Pacagroup.Ecommerce.Domain.Entity;



namespace Pacagroup.Ecommerce.Infrastructure.Interface;



/// <summary>
/// Interfaz del repositorio de categorías
/// </summary>
public interface ICategoriesRepository 
{
    Task<IEnumerable<Categories>> GetAll();

}