using System;
using BICE.BLL;

namespace BICE.DAL
{
    public class VehicleIntervention_DAL
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int InterventionId { get; set; }
        
        public VehicleIntervention_DAL(int vehicleId, int interventionId)
        {
            VehicleId = vehicleId;
            InterventionId = interventionId;
        }
        
        public VehicleIntervention_DAL(int id, int vehicleId, int interventionId)
            : this(vehicleId, interventionId)
        {
            Id = id;
        }
        
        public VehicleIntervention_DAL(VehicleIntervention_BLL vehicleIntervention)
            : this(vehicleIntervention.IdVehicle, vehicleIntervention.IdIntervention)
        {
        }
        
        public VehicleIntervention_DAL(int id, VehicleIntervention_BLL vehicleIntervention)
            : this(id, vehicleIntervention.IdVehicle, vehicleIntervention.IdIntervention)
        {
        }
        
        public VehicleIntervention_BLL ToBLL()
        {
            return new VehicleIntervention_BLL(VehicleId, InterventionId);
        }

    }
}