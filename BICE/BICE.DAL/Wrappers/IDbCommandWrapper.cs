using System.Data;

namespace BICE.DAL.Wrappers
{
    public interface IDbCommandWrapper : IDisposable
    {
        string CommandText { get; set; }
        void ExecuteNonQuery();
        IDataReader ExecuteReader();
        object ExecuteScalar();
        void Dispose();
        // add other necessary methods/properties like Parameters

        IDataParameter CreateParameter(); // Add this method
    }
}