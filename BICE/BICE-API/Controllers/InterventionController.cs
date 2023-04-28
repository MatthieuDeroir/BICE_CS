using System.Collections.Generic;
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

        public InterventionController()
        {
            _interventionService = new Intervention_SRV();
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
        
        [HttpPut]
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