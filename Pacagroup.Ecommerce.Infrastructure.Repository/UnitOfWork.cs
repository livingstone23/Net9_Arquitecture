using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase que implementa el patrón Unit of Work
/// </summary>
public class UnitOfWork: IUnitOfWork
{
    

    public ICustomersRepository Customers { get; }
    
    public IUsersRepository Users { get; }

    public ICategoriesRepository Categories { get; set; }


    public UnitOfWork(ICustomersRepository customers, IUsersRepository users, ICategoriesRepository categories)
    {
        Customers = customers;
        Users = users;
        Categories = categories;
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }

    
}