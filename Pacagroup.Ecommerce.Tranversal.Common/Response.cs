namespace Pacagroup.Ecommerce.Tranversal.Common;



/// <summary>
/// Clase genérica para las respuestas
/// </summary>
/// <typeparam name="T"></typeparam>
public class Response<T>
{
    
    public T Data { get; set; }
    
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

}