using System;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace BICE.BLL;

public class VehicleMaterial_BLL
{
    public int Id { get; set; }
    [Required(ErrorMessage = "VehicleId is required !")]
    public int VehicleId { get; set; }
    
    [Required(ErrorMessage = "MaterialId is required !")]
    public int MaterialId { get; set; }

    public VehicleMaterial_BLL(int id, int vehicleId, int materialId)
    {
        Id = id;
        VehicleId = vehicleId;
        MaterialId = materialId;
    }
}