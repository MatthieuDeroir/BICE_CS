namespace BICE.BLL.Interfaces
{
    public interface IVehicle_BLL
    {
        int Id { get; }
        string Denomination { get; }
        string InternalNumber { get; }
        string LicensePlate { get; }
        bool IsActive { get; }
    }
}