using System.Data;
using System.Data.SqlClient;

namespace BICE.DAL.Wrappers;

public class DbConnectionWrapper : IDbConnectionWrapper
{
    private SqlConnection _connection;

    public DbConnectionWrapper(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public IDbCommandWrapper CreateCommand()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    
        return new DbCommandWrapper(_connection.CreateCommand());
    }


    public void Open()
    {
        _connection.Open();
    }

    public void Close()
    {
        _connection.Close();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}