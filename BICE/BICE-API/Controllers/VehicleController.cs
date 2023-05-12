using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;





namespace BICE.API.Controllers
{
    
    /// <summary>
    /// VehicleController :
    /// Obtenir tous les véhicules (GetVehicle)
    /// Obtenir un véhicule spécifique par son ID (GetVehicleById)
    /// Obtenir les véhicules liés à une intervention spécifique (GetVehiclesByInterventionId)
    /// Obtenir un véhicule spécifique par son numéro interne (GetVehicleByInternalNumber)
    /// Ajouter un nouveau véhicule (AddVehicle)
    /// Mettre à jour un véhicule (UpdateVehicle)
    /// Désactiver un véhicule (DisableVehicle)
    /// Activer un véhicule (EnableVehicle)
    /// Supprimer un véhicule (DeleteVehicle)
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly Vehicle_SRV _vehicleService;

        public VehicleController()
        {
            _vehicleService = new Vehicle_SRV();
        }
        
        // GET api/vehicle
        // Obtenir tous les véhicules

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle_DTO>> GetVehicle()
        {
            IEnumerable<Vehicle_DTO> vehicleDto = _vehicleService.GetVehicle();
            return Ok(vehicleDto);
        }
        
        // GET api/vehicle/{id}
        // Obtenir un véhicule spécifique par son ID
        
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
        
        // GET api/vehicle/{interventionId}/vehicles
        // Obtenir les véhicules liés à une intervention spécifique
        
        [HttpGet("in-intervention/{interventionId}")]
        public ActionResult<Vehicle_DTO> GetVehiclesByInterventionId(int interventionId)
        {
            IEnumerable<Vehicle_DTO> vehicleDto = _vehicleService.GetVehiclesByInterventionId(interventionId);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleDto);
        }

        // GET api/vehicle/{internalNumber}
        // Obtenir un véhicule spécifique par son numéro interne
        
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
        
        // GET api/vehicle/{licensePlate}
        // Obtenir un véhicule spécifique par sa plaque d'immatriculation

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
        
        // PUT api/vehicle
        // Mettre à jour un véhicule

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
        
        // PUT api/disable/{id}
        // Désactiver un véhicule spécifique par son ID
        
        [HttpPut("disable/{id}")]
        public ActionResult<Vehicle_DTO> DisableVehicle(int id)
        {
            Vehicle_DTO vehicleDto = _vehicleService.GetVehicleById(id);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            vehicleDto.IsActive = false;
            Vehicle_DTO updatedVehicle = _vehicleService.Update(vehicleDto);
            return Ok(updatedVehicle);
        }
        
        // PUT api/enable/{id}
        // Activer un véhicule spécifique par son ID
        
        [HttpPut("enable/{id}")]
        public ActionResult<Vehicle_DTO> EnableVehicle(int id)
        {
            Vehicle_DTO vehicleDto = _vehicleService.GetVehicleById(id);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            vehicleDto.IsActive = true;
            Vehicle_DTO updatedVehicle = _vehicleService.Update(vehicleDto);
            return Ok(updatedVehicle);
        }
        
        // DELETE api/vehicle/{id}
        // Supprimer un véhicule spécifique par son ID
        
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