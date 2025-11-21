using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase que implementa el patrón Unit of Work
/// </summary>
public class UnitOfWork: IUnitOfWork
{
    

    public ICustomersRepository Customers { get; }
    public IUsersRepository Users { get; }


    public UnitOfWork(ICustomersRepository customers, IUsersRepository users)
    {
        Customers = customers;
        Users = users;
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }

    
}