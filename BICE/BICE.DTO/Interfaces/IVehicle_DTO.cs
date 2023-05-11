using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO.Interfaces;

public interface IVehicle_DTO
{
    int Id { get; set; }
    String Denomination { get; set; }
    String InternalNumber { get; set; }
    String LicensePlate { get; set; }
    Boolean IsActive { get; set; }
    Vehicle_BLL ToBLL();
    Vehicle_DAL ToDAL();
}