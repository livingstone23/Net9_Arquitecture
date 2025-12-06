using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Domain.Core;



/// <summary>
/// Implementación de la lógica de negocio para las categorías
/// </summary>
public class CategoriesDomain: ICategoriesDomain
{

    /// <summary>
    /// Propiedad para implementar el patrón Unit of Work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<Categories>> GetAll()
    {
        return await _unitOfWork.Categories.GetAll();
    }

}