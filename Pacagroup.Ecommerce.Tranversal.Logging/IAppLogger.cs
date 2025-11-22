


namespace Pacagroup.Ecommerce.Tranversal.Logging;



/// <summary>
/// Interface para el manejo de logs
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAppLogger<T>
{

    void LogInformation(string message, params object[] args);

    void LogWarning(string message, params object[] args);

    void LogError(string message, params object[] args);

    void LogError(Exception ex, string message, params object[] args);

    void LogDebug(string message, params object[] args);
    
}
