using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Tranversal.Common;



namespace Pacagroup.Ecommerce.Application.Interface;



/// <summary>
/// Interface para la gestión de categorías
/// </summary>
public interface ICategoriesApplication
{

    Task<Response<IEnumerable<CategoriesDto>>> GetAll();

}