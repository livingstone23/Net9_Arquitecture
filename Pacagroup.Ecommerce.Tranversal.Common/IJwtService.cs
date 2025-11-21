using Pacagroup.Ecommerce.Domain.Entity;



namespace Pacagroup.Ecommerce.Tranversal.Common;



public interface IJwtService
{
    string GenerateToken(User user);

}