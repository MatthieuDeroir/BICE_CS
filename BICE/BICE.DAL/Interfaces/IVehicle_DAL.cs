using BICE.BLL;

namespace BICE.DAL
{
    public interface IVehicle_DAL
    {
        int Id { get; set; }
        string Denomination { get; set; }
        string InternalNumber { get; set; }
        string LicensePlate { get; set; }
        bool IsActive { get; set; }
        
        Vehicle_BLL ToBLL();
    }
}