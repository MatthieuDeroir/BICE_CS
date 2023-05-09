using System;
using BICE.BLL;



namespace BICE.DAL;

// VehicleMaterial_DAL.cs
public class VehicleMaterial_DAL
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int MaterialId { get; set; }

    public VehicleMaterial_DAL(int vehicleId, int materialId)
    {
        VehicleId = vehicleId;
        MaterialId = materialId;
    }

    public VehicleMaterial_DAL(int id, int vehicleId, int materialId) : this(vehicleId, materialId)
    {
        Id = id;
    }

    public VehicleMaterial_DAL(VehicleMaterial_BLL vehicleMaterial) : this(vehicleMaterial.VehicleId, vehicleMaterial.MaterialId)
    {
    }

    public VehicleMaterial_DAL(int id, VehicleMaterial_BLL vehicleMaterial) : this(id, vehicleMaterial.VehicleId, vehicleMaterial.MaterialId)
    {
    }
    

    public VehicleMaterial_BLL ToBLL()
    {
        return new VehicleMaterial_BLL(Id, VehicleId, MaterialId);
    }
}