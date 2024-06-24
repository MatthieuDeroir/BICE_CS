using System.Collections.Generic;
using BICE.DAL.Wrappers;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;

namespace BICE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class InterventionController : ControllerBase
    {
        private readonly Intervention_SRV _interventionService;

        public InterventionController(IDbConnectionWrapper connection, IDbCommandWrapper command)
        {
            _interventionService = new Intervention_SRV(connection, command);
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Intervention_DTO>> GetIntervention()
        {
            IEnumerable<Intervention_DTO> interventionDto = _interventionService.GetIntervention();
            return Ok(interventionDto);
        }
        
        [HttpGet("{id}")]
        public ActionResult<Intervention_DTO> GetInterventionById(int id)
        {
            Intervention_DTO interventionDto = _interventionService.GetInterventionById(id);
            if (interventionDto == null)
            {
                return NotFound();
            }
            return Ok(interventionDto);
        }
        

        // POST api/intervention
        [HttpPost]
        public ActionResult<Intervention_DTO> AddIntervention(Intervention_DTO interventionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Intervention_DTO insertedIntervention = _interventionService.AddIntervention(interventionDto);
            return CreatedAtAction(nameof(AddIntervention), new { id = insertedIntervention.Id }, insertedIntervention);
        }
        
        [HttpGet("{interventionId}/vehicles")]
        public ActionResult<Intervention_DTO> GetVehiclesByInterventionId(int interventionId)
        {
            IEnumerable<Vehicle_DTO> vehicleDto = _interventionService.GetVehiclesByInterventionId(interventionId);
            if (vehicleDto == null)
            {
                return NotFound();
            }
            return Ok(vehicleDto);
        }
        
        // add vehicle to intervention
        [HttpPost("{interventionId}/vehicle/{vehicleId}")]
        public async Task<ActionResult> AddVehicleToIntervention(int interventionId, int vehicleId)
        {
            await _interventionService.AddVehicleToIntervention(interventionId, vehicleId);
            return NoContent();
        }

        
        [HttpPut("{id}")]
        public ActionResult<Intervention_DTO> UpdateIntervention(Intervention_DTO interventionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Intervention_DTO updatedIntervention = _interventionService.Update(interventionDto);
            return Ok(updatedIntervention);
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteIntervention(Intervention_DTO interventionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _interventionService.Delete(interventionDto);
            return Ok();
        }


    }
}