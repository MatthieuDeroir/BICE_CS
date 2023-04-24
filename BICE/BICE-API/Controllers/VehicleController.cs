using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;

namespace BICE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly Vehicle_SRV _vehicleService;

        public VehicleController()
        {
            _vehicleService = new Vehicle_SRV();
        }

        // POST api/vehicle
        [HttpPost]
        public ActionResult<Vehicle_DTO> AddVehicle(Vehicle_DTO vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vehicle_DTO insertedVehicle = _vehicleService.AddVehicle(vehicleDto);
            return CreatedAtAction(nameof(AddVehicle), new { id = insertedVehicle.Id }, insertedVehicle);
        }

        // Implement other CRUD methods using the service
    }
}