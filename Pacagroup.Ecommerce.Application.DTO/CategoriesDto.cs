namespace Pacagroup.Ecommerce.Application.DTO;



/// <summary>
/// Clase DTO para las categorías
/// </summary>
public class CategoriesDto
{
    
    public int CategoryID { get; set; }
    
    public string CategoryName { get; set; }
    
    public string Description { get; set; }

    public byte[] Picture { get; set; }

}