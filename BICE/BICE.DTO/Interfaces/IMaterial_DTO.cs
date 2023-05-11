using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO.Interfaces;

public interface IMaterial_DTO
{
    int Id { get; set; }
    String Denomination { get; set; }
    String Barcode { get; set; }
    String Category { get; set; }
    int UsageCount { get; set; }
    int? MaxUsageCount { get; set; }
    DateTime? ExpirationDate { get; set; }
    DateTime? NextControlDate { get; set; }
    Boolean IsStored { get; set; }
    Boolean IsLost { get; set; }
    Boolean IsRemoved { get; set; }
    int? VehicleId { get; set; }
    String VehicleDenomination { get; set; }
    String VehicleInternalNumber { get; set; }
    String VehicleLicensePlate { get; set; }
    Material_BLL ToBLL();
    Material_DAL ToDAL();
}