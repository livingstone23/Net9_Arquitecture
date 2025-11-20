namespace Pacagroup.Ecommerce.Application.DTO;



/// <summary>
/// Clase DTO para el cliente
/// Es de tipo inmutable (record) dado que los DTOs no deben cambiar su estado una vez creados
/// </summary>
public sealed record CustomerDto
{
    public string? CustomerId { get; set; }
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
}