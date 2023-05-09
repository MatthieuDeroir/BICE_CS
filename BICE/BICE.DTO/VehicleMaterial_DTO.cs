using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO;

public class VehicleMaterial_DTO
{
    public int Id { get; set; }
    public int VehicleID { get; set; }
    public int InterventionID { get; set; }
    
    public VehicleMaterial_DTO()
    {
    }
    
    public VehicleMaterial_DTO(int vehicleId, int interventionId)
    {
        VehicleID = vehicleId;
        InterventionID = interventionId;
    }

    public VehicleMaterial_DTO(VehicleMaterial_DAL vehicleMaterialDal)
    {
        Id = vehicleMaterialDal.Id;
        VehicleID = vehicleMaterialDal.VehicleId;
        InterventionID = vehicleMaterialDal.MaterialId;
    }

    public VehicleMaterial_DTO(VehicleMaterial_DTO vehicleMaterial) : this(vehicleMaterial.VehicleID, vehicleMaterial.InterventionID)
    {
    }
    
    public VehicleMaterial_DTO(int id, VehicleMaterial_DTO vehicleMaterial) : this(vehicleMaterial.VehicleID, vehicleMaterial.InterventionID)
    {
    }

    public VehicleMaterial_DTO(int id, int vehicleId, int interventionId) : this(vehicleId, interventionId)
    {
        Id = id;
    }
    
    public VehicleMaterial_BLL ToBLL()
    {
        return new VehicleMaterial_BLL(Id, VehicleID, InterventionID);
    }
    
    public VehicleMaterial_DAL ToDAL()
    {
        return new VehicleMaterial_DAL(Id, VehicleID, InterventionID);
    }
    
    
}