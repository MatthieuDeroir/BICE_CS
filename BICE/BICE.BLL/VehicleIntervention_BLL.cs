using System;
using System.ComponentModel.DataAnnotations;

namespace BICE.BLL
{
	public class VehicleIntervention_BLL
	{
		[Required(ErrorMessage = "ID is required !")]
		public int Id { get; set; }
		[Required(ErrorMessage = "Vehicle ID is required !")]
        public int VehicleId { get; private set; }

        [Required(ErrorMessage = "Intervention ID is required !")]
        public int InterventionId { get; private set; }

        public VehicleIntervention_BLL(int id, int vehicleId, int interventionId)
        {
	        Id = id;
			VehicleId = vehicleId;
			InterventionId = interventionId;
		}
	}
}

