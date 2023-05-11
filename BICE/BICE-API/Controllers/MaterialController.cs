using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BICE.DTO;
using BICE.SRV;

namespace BICE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MaterialController : ControllerBase
    {   
        private readonly Material_SRV _materialService;
        
        public MaterialController()
        {
            _materialService = new Material_SRV();
        }
        
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
        
        // Endpoint to get the list of stored materials with their vehicle information
        [HttpGet("stored-materials")]
        public ActionResult<IEnumerable<Material_DTO>> GetStoredMaterials()
        {
            IEnumerable<Material_DTO> materials = _materialService.GetStoredMaterials();
            return Ok(materials);
        }

        // Endpoint to get the list of materials to be removed from stock
        [HttpGet("materials-to-be-removed")]
        public ActionResult<IEnumerable<Material_DTO>> GetMaterialsToBeRemoved()
        {
            IEnumerable<Material_DTO> materials = _materialService.GetMaterialsToBeRemoved();
            return Ok(materials);
        }

        // Endpoint to get the list of materials to be checked
        [HttpGet("materials-to-be-checked")]
        public ActionResult<IEnumerable<Material_DTO>> GetMaterialsToBeChecked()
        {
            IEnumerable<Material_DTO> materials = _materialService.GetMaterialsToBeChecked();
            return Ok(materials);
        }
    
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
        
        // [HttpPost("history")]
        // public ActionResult<>

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
