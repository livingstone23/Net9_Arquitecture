using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase que implementa el patrón Unit of Work
/// </summary>
public class UnitOfWork: IUnitOfWork
{

    public UnitOfWork(ICustomersRepository customers)
    {
        Customers = customers;
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }

    public ICustomersRepository Customers { get; }

}