

using System.Data;
using System.Data.SqlClient;

namespace BICE.DAL.Wrappers
{
    public interface IDbConnectionWrapper : IDisposable
    {
        IDbCommandWrapper CreateCommand();
        void Open();
        void Close();
    }

}

