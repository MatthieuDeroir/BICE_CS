using System.Data.Common;

namespace BICE.DAL.Wrappers;

public class DbDataReaderWrapper : IDbDataReaderWrapper
{
    private readonly DbDataReader _reader;

    public DbDataReaderWrapper(DbDataReader reader)
    {
        _reader = reader;
    }

    public bool Read()
    {
        return _reader.Read();
    }

    public object this[string name]
    {
        get { return _reader[name]; }
    }
    // other necessary methods
}
