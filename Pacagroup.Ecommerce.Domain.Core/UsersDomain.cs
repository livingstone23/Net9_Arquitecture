using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Domain.Core;



/// <summary>
/// Clase que implementa la lógica de negocio para los usuarios 
/// </summary>
public class UsersDomain : IUsersDomain
{

    /// <summary>
    /// Propiedad para implementar el patrón Unit of Work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    public UsersDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _unitOfWork.Users.CheckPasswordAsync(user, password);
    }

    public async Task<bool> CreateUserAsync(User user, string password)
    {
        return await _unitOfWork.Users.CreateUserAsync(user, password);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _unitOfWork.Users.GetByEmailAsync(email);
    }

}