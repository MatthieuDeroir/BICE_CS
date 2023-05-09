using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;

namespace BICE.API.Controllers
{
    public class VehicleMaterialController : ControllerBase
    {
        private readonly VehicleMaterial_SRV _vehicleMaterialService;
        
        public VehicleMaterialController()
        {
            _vehicleMaterialService = new VehicleMaterial_SRV();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<VehicleMaterial_DTO>> GetVehicleMaterial()
        {
            IEnumerable<VehicleMaterial_DTO> vehicleMaterialDto = _vehicleMaterialService.GetVehicleMaterial();
            return Ok(vehicleMaterialDto);
        }
        
        [HttpGet("{id}")]
        public ActionResult<VehicleMaterial_DTO> GetVehicleMaterialById(int id)
        {
            VehicleMaterial_DTO vehicleMaterialDto = _vehicleMaterialService.GetVehicleMaterialById(id);
            if (vehicleMaterialDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleMaterialDto);
        }
        
        [HttpGet("{vehicleId}")]
        public ActionResult<VehicleMaterial_DTO> GetVehicleMaterialByVehicleId(int vehicleId)
        {
            VehicleMaterial_DTO vehicleMaterialDto = _vehicleMaterialService.GetByVehicleId(vehicleId);
            if (vehicleMaterialDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleMaterialDto);
        }
        
        [HttpPost]
        public ActionResult<VehicleMaterial_DTO> AddVehicleMaterial(VehicleMaterial_DTO vehicleMaterialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleMaterial_DTO insertedVehicleMaterial = _vehicleMaterialService.AddVehicleMaterial(vehicleMaterialDto);
            return CreatedAtAction(nameof(AddVehicleMaterial), new { id = insertedVehicleMaterial.Id }, insertedVehicleMaterial);
        }
        
        [HttpPut("{id}")]
        public ActionResult<VehicleMaterial_DTO> UpdateVehicleMaterial(VehicleMaterial_DTO vehicleMaterialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleMaterial_DTO updatedVehicleMaterial = _vehicleMaterialService.Update(vehicleMaterialDto);
            return Ok(updatedVehicleMaterial);
        }
        
        [HttpDelete("{id}")]
        public ActionResult<VehicleMaterial_DTO> DeleteVehicleMaterial(int id)
        {
            VehicleMaterial_DTO vehicleMaterialDto = _vehicleMaterialService.GetVehicleMaterialById(id);
            if (vehicleMaterialDto == null)
            {
                return NotFound();
            }
            _vehicleMaterialService.Delete(vehicleMaterialDto);
            return Ok();
        }
        
    }
    
    
    
    
    
}