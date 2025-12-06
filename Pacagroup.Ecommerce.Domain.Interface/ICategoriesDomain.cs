using Pacagroup.Ecommerce.Domain.Entity;



namespace Pacagroup.Ecommerce.Domain.Interface;



/// <summary>
/// Interface para la gestión de categorías 
/// </summary>
public interface ICategoriesDomain
{

    Task<IEnumerable<Categories>> GetAll();

}