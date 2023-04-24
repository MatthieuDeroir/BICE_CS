using System;
using System.ComponentModel.DataAnnotations;

namespace BICE.BLL
{
	public class VehicleIntervention_BLL
	{
		[Required(ErrorMessage = "Vehicle ID is required !")]
        public int IdVehicle { get; private set; }

        [Required(ErrorMessage = "Intervention ID is required !")]
        public int IdIntervention { get; private set; }

        public VehicleIntervention_BLL(int _IdVehicle, int _IdIntervention)
		{
			IdVehicle = _IdVehicle;
			IdIntervention = _IdIntervention;
		}
	}
}

