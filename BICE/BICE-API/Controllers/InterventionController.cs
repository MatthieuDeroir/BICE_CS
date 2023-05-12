using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;



namespace BICE.API.Controllers
{
    
    /// <summary>
    /// InterventionController :
    /// Obtenir toutes les interventions (GetIntervention)
    /// Obtenir une intervention spécifique par son ID (GetInterventionById)
    /// Ajouter une nouvelle intervention (AddIntervention)
    /// Ajouter un véhicule à une intervention spécifique (AddVehicleToIntervention)
    /// Mettre à jour une intervention (UpdateIntervention)
    /// Supprimer une intervention (DeleteIntervention)
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class InterventionController : ControllerBase
    {
        private readonly Intervention_SRV _interventionService;

        public InterventionController()
        {
            _interventionService = new Intervention_SRV();
        }
        
        // GET api/intervention
        // Obtenir toutes les interventions
        
        [HttpGet]
        public ActionResult<IEnumerable<Intervention_DTO>> GetIntervention()
        {
            IEnumerable<Intervention_DTO> interventionDto = _interventionService.GetIntervention();
            return Ok(interventionDto);
        }
        
        // GET api/intervention/{id}
        // Obtenir une intervention spécifique par son ID
        
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
        // Ajouter une nouvelle intervention
        
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

        // POST api/intervention/{interventionId}/vehicle/{vehicleId}
        // Ajouter un véhicule à une intervention spécifique
        
        [HttpPost("{interventionId}/vehicle/{vehicleId}")]
        public async Task<ActionResult> AddVehicleToIntervention(int interventionId, int vehicleId)
        {
            await _interventionService.AddVehicleToIntervention(interventionId, vehicleId);
            return NoContent();
        }

        // PUT api/intervention/{id}
        // Mettre à jour une intervention
        
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
        
        // DELETE api/intervention/{id}
        // Supprimer une intervention
        
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
        
        // DELETE api/intervention/{interventionId}/vehicle/{vehicleId}
        // Supprimer un véhicule d'une intervention spécifique
        
        [HttpDelete("{interventionId}/vehicle/{vehicleId}")]
        public async Task<ActionResult> DeleteVehicleFromIntervention(int interventionId, int vehicleId)
        {
            _interventionService.DeleteVehicleFromIntervention(interventionId, vehicleId);
            return NoContent();
        }
    }
}