using BICE.DAL;
using BICE.DTO.Interfaces;

namespace BICE.DTO;

public class MaterialVehicle_DTO : Material_DTO, IMaterialVehicle_DTO
{
    
    	
    //variable only used by the export with the join of the vehicle
    public String VehicleDenomination { get; set; }
    public String VehicleInternalNumber { get; set; }
    public String VehicleLicensePlate { get; set; }


    public MaterialVehicle_DTO()
    {
        
    }

    public MaterialVehicle_DTO(Material_DAL materialDal, string vehicleInternalNumber, string vehicleDenomination, string vehicleLicensePlate)
    {
        Id = materialDal.Id;
        Denomination = materialDal.Denomination;
        Barcode = materialDal.Barcode;
        Category = materialDal.Category;
        UsageCount = materialDal.UsageCount;
        MaxUsageCount = materialDal.MaxUsageCount;
        ExpirationDate = materialDal.ExpirationDate;
        NextControlDate = materialDal.NextControlDate;
        IsStored = materialDal.IsStored;
        IsLost = materialDal.IsLost;
        IsRemoved = materialDal.IsRemoved;
        VehicleId = materialDal.VehicleId;
        VehicleDenomination = vehicleDenomination; // Assign the vehicle denomination from the joined data
        VehicleInternalNumber = vehicleInternalNumber; // Assign the vehicle number from the joined data
        VehicleLicensePlate = vehicleLicensePlate; // Assign the vehicle license plate from the joined data
    }
}