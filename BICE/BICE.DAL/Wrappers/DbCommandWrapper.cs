using System.Data;
using System.Data.SqlClient;
using BICE.DAL.Wrappers;

namespace BICE.DAL.Wrappers
{


    public class DbCommandWrapper : IDbCommandWrapper
    {
        private SqlCommand _command;

        public DbCommandWrapper(SqlCommand command)
        {
            _command = command;
        }

        public string CommandText
        {
            get => _command.CommandText;
            set => _command.CommandText = value;
        }

        public void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            if (_command.Connection.State != ConnectionState.Open)
            {
                _command.Connection.Open();
            }

            return _command.ExecuteReader();
        }


        public object ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }

        public void Dispose()
        {
            _command.Dispose();
        }
        // implement other necessary methods/properties like Parameters
        public IDataParameter CreateParameter()
        {
            return _command.CreateParameter();
        }
    }
}