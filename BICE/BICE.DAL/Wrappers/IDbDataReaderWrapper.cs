namespace BICE.DAL.Wrappers;

public interface IDbDataReaderWrapper
{
    bool Read();
    object this[string name] { get; }
    // other necessary methods
}