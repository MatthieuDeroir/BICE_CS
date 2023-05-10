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
        public ActionResult<Material_DTO> InsertMaterialList(IEnumerable<Material_DTO> materialDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Material_DTO> insertedMaterials = _materialService.AddMaterials(materialDtos);
            return Ok(insertedMaterials);
        }
        
        
        [HttpPut("vehicle-preparation/{vehicleId}")]
        public IActionResult PrepareVehicle(int vehicleId, [FromBody] List<string> barcodes)
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
