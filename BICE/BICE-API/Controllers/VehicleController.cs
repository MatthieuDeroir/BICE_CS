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
        
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle_DTO>> GetVehicle()
        {
            IEnumerable<Vehicle_DTO> vehicleDto = _vehicleService.GetVehicle();
            return Ok(vehicleDto);
        }
        
        [HttpGet("{id}")]
        public ActionResult<Vehicle_DTO> GetVehicleById(int id)
        {
            Vehicle_DTO vehicleDto = _vehicleService.GetVehicleById(id);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleDto);
        }
        
        
        
        [HttpGet("{internalNumber}")]
        public ActionResult<Vehicle_DTO> GetVehicleByInternalNumber(string internalNumber)
        {
            Vehicle_DTO vehicleDto = _vehicleService.GetVehicleByInternalNumber(internalNumber);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleDto);
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
        
        
        [HttpPut]
        public ActionResult<Vehicle_DTO> UpdateVehicle(Vehicle_DTO vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vehicle_DTO updatedVehicle = _vehicleService.Update(vehicleDto);
            return Ok(updatedVehicle);
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteVehicle(int id)
        {
            Vehicle_DTO vehicleDto = _vehicleService.GetVehicleById(id);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            _vehicleService.Delete(vehicleDto);
            return NoContent();
        }
        
        
    }
}