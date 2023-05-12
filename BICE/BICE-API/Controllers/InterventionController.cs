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
            try
            {
                Intervention_DTO interventionDto = _interventionService.GetInterventionById(id);
                return Ok(interventionDto); 
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
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

            try
            {
                Intervention_DTO insertedIntervention = _interventionService.AddIntervention(interventionDto);
                return Ok(insertedIntervention);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        // POST api/intervention/{interventionId}/vehicle/{vehicleId}
        // Ajouter un véhicule à une intervention spécifique
        // Gestion des erreurs :
        
        [HttpPost("{interventionId}/vehicle/{vehicleId}")]
        public ActionResult AddVehicleToIntervention(int interventionId, int vehicleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var addedVehiculeToIntervention= _interventionService.AddVehicleToIntervention(interventionId, vehicleId);
                return Ok(addedVehiculeToIntervention);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
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

            try
            {
                Intervention_DTO updatedIntervention = _interventionService.Update(interventionDto);
                return Ok(updatedIntervention);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }

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

            try
            {
                _interventionService.Delete(interventionDto);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            
        }
        
        // DELETE api/intervention/{interventionId}/vehicle/{vehicleId}
        // Supprimer un véhicule d'une intervention spécifique
        
        [HttpDelete("{interventionId}/vehicle/{vehicleId}")]
        public ActionResult DeleteVehicleFromIntervention(int interventionId, int vehicleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _interventionService.DeleteVehicleFromIntervention(interventionId, vehicleId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            ;
        }
    }
}