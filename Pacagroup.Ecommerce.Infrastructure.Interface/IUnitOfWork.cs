


namespace Pacagroup.Ecommerce.Infrastructure.Interface;



/// <summary>
/// Clase que permite implementar el patrón Unit of Work
/// </summary>
public interface IUnitOfWork : IDisposable
{

    ICustomersRepository Customers { get; }
    
    IUsersRepository Users { get; }

    ICategoriesRepository Categories { get; }

}