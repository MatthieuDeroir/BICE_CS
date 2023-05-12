using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;

namespace BICE.API.Controllers
{
    
    /// <summary>
    /// MaterialController :
    /// Obtenir tous les matériaux (GetMaterial)
    /// Obtenir un matériau spécifique par son ID (GetMaterialById)
    /// Obtenir les matériaux liés à un véhicule spécifique (GetMaterialByVehicleId)
    /// Obtenir un matériau spécifique par son code-barres (GetMaterialByBarcode)
    /// Obtenir une liste de matériaux stockés (GetStoredMaterials)
    /// Obtenir une liste de matériaux à retirer (GetMaterialsToBeRemoved)
    /// Obtenir une liste de matériaux à vérifier (GetMaterialsToBeChecked)
    /// Obtenir l'historique d'utilisation d'un matériau spécifique (GetMaterialUsageHistoryByMaterialId)
    /// Ajouter un nouveau matériau (AddMaterial)
    /// Ajouter une liste de matériaux (InsertMaterialList)
    /// Gérer le retour de matériaux d'une intervention (ReturnMaterialFromIntervention)
    /// Préparer un véhicule avec une liste de codes-barres de matériaux (PrepareVehicle)
    /// Mettre à jour un matériau (UpdateMaterial)
    /// Supprimer un matériau (DeleteMaterial)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    public class MaterialController : ControllerBase
    {   
        private readonly Material_SRV _materialService;
        
        public MaterialController()
        {
            _materialService = new Material_SRV();
        }
        // GET api/material
        // Obtenir tous les matériaux
        
        [HttpGet]
        public ActionResult<IEnumerable<Material_DTO>> GetMaterial()
        {
            IEnumerable<Material_DTO> materialDto = _materialService.GetMaterial();
            if (materialDto == null)
            {
                return NotFound();
            }
            return Ok(materialDto);
        }
        
        // GET api/material/{id}
        // Obtenir un matériau spécifique par son ID
        
        [HttpGet("{id}")]
        public ActionResult<Material_DTO> GetMaterialById(int id)
        {
            Material_DTO materialDto = _materialService.GetMaterialById(id);
            if (materialDto == null)
            {
                return NotFound();
            }
            return Ok(materialDto);
        }
        // GET api/material/vehicle/{vehicleId}
        // Obtenir les matériaux liés à un véhicule spécifique
        
        [HttpGet("vehicle/{vehicleId}")]
        public ActionResult<Material_DTO> GetMaterialByVehicleId(int vehicleId)
        {
            IEnumerable<Material_DTO> materialDto = _materialService.GetMaterialByVehicleId(vehicleId);
            
            if (materialDto == null)
            {
                return NotFound();
            }
            
            return Ok(materialDto);
        }
        
        // GET api/material/barcode/{barcode}
        // Obtenir un matériau spécifique par son code-barres
        [HttpGet("barcode/{barcode}")]
        public ActionResult<Material_DTO> GetMaterialByBarcode(string barcode)
        {
            Material_DTO materialDto = _materialService.GetMaterialByBarcode(barcode);
            if (materialDto == null)
            {
                return NotFound();
            }
            return Ok(materialDto);
        }
        
        // GET api/material/stored-materials
        // Obtenir une liste de matériaux stockés et présents dans les vehicles
        [HttpGet("stored-materials")]
        public ActionResult<IEnumerable<MaterialVehicle_DTO>> GetStoredMaterials()
        {
            IEnumerable<MaterialVehicle_DTO> materials = _materialService.GetStoredMaterials();
            return Ok(materials);
        }

        // GET api/material/materials-to-be-removed
        // Obtenir une liste de matériaux à retirer
        
        [HttpGet("materials-to-be-removed")]
        public ActionResult<IEnumerable<Material_DTO>> GetMaterialsToBeRemoved()
        {
            IEnumerable<Material_DTO> materials = _materialService.GetMaterialsToBeRemoved();
            return Ok(materials);
        }

        // GET api/material/materials-to-be-checked
        // Obtenir une liste de matériaux à vérifier
        
        [HttpGet("materials-to-be-checked")]
        public ActionResult<IEnumerable<Material_DTO>> GetMaterialsToBeChecked()
        {
            IEnumerable<Material_DTO> materials = _materialService.GetMaterialsToBeChecked();
            return Ok(materials);
        }
        
        // GET api/material/history/{materialId}
        // Obtenir l'historique d'utilisation d'un matériau spécifique
    
        [HttpGet("history/{materialId}")]
        public ActionResult<IEnumerable<MaterialUsageHistory_DTO>> GetMaterialUsageHistoryByMaterialId(int materialId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IEnumerable<MaterialUsageHistory_DTO> materialHistoryDto = _materialService.GetMaterialUsageHistory(materialId);
            return Ok(materialHistoryDto);
        }
        
        // POST api/material
        // Ajouter un nouveau matériau

        [HttpPost]
        public ActionResult<Material_DTO> AddMaterial(Material_DTO materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Material_DTO insertedMaterial = _materialService.AddMaterial(materialDto);
            return CreatedAtAction(nameof(AddMaterial), new { id = insertedMaterial.Id }, insertedMaterial);
        }
        
        // POST api/material/insert-list
        // Ajouter une liste de matériaux

        [HttpPost("insert-list")]
        public ActionResult<Material_DTO> InsertMaterialList([FromBody]IEnumerable<Material_DTO> materialDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Material_DTO> insertedMaterials = _materialService.AddMaterials(materialDtos);
            return Ok(insertedMaterials);
        }
        
        // PUT api/intervention-return/{interventionId}/{vehicleId}
        // Gérer le retour de matériaux d'une intervention
        
        [HttpPost("intervention-return/{interventionId}/{vehicleId}")]
        public ActionResult<Material_DTO> ReturnMaterialFromIntervention(int interventionId, int vehicleId, InterventionReturn_DTO interventionReturnDto)
        {
            try
            {
                var returnedMaterials = _materialService.HandleInterventionReturn(interventionId, vehicleId, interventionReturnDto);
                return Ok(returnedMaterials);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error returning material from intervention: {ex.Message}");
            }
        }
        
        // PUT api/vehicle-preparation/{vehicleId}
        // Préparer un véhicule pour une intervention
        
        [HttpPut("vehicle-preparation/{vehicleId}")]
        public IActionResult PrepareVehicle(int vehicleId, List<string> barcodes)
        {
            try
            {
                var preparedMaterials = _materialService.PrepareVehicle(vehicleId, barcodes);
                return Ok(preparedMaterials);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error preparing vehicle: {ex.Message}");
            }
        }
        
        // PUT api/material
        // Mettre à jour un matériau

        [HttpPut]
        public ActionResult<Material_DTO> UpdateMaterial(Material_DTO materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Material_DTO updatedMaterial = _materialService.Update(materialDto);
            return Ok(updatedMaterial);
        }
        
        // DELETE api/material/{id}
        // Supprimer un matériau
        
        [HttpDelete("{id}")]
        public ActionResult<Material_DTO> DeleteMaterial(int id)
        {
            Material_DTO materialDto = _materialService.GetMaterialById(id);
            if (materialDto == null)
            {
                return NotFound();
            }

            _materialService.Delete(materialDto);
            return Ok();
        }
        
        
    }
}
