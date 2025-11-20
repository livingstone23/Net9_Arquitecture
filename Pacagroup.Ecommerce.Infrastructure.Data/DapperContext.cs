using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace Pacagroup.Ecommerce.Infrastructure.Data;



/// <summary>
/// Clase que permite la conexión a la base de datos utilizando Dapper
/// </summary>
/// La libreria Extension permite leer el archivo de configuración appsettings.json
public class DapperContext
{


    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("NorthwindConnection");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);


}