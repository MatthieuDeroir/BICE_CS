using System;
using System.Collections;
using BICE.DAL;
using BICE.DTO;
using BICE.BLL;

namespace BICE.SRV
{
	public class Material_SRV
	{
		private readonly Material_Repository _materialRepository;
		public Material_SRV()
		{
			_materialRepository = new Material_Repository();
		}
		
public IEnumerable<Material_DTO> GetMaterial()
		{
			IEnumerable<Material_DAL> materialDal = _materialRepository.GetAll();
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDal)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}

		public Material_DTO GetMaterialById(int id)
		{
			Material_DAL materialDal = _materialRepository.GetById(id);
			return new Material_DTO(materialDal);
		}

		public IEnumerable<Material_DTO> GetMaterialByVehicleId(int id)
		{
			IEnumerable<Material_DAL> materialDal = _materialRepository.GetByVehicleId(id);
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDal)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}
		
		public Material_DTO GetMaterialByBarcode(string barcode)
		{
			Material_DAL materialDal = _materialRepository.GetByBarcode(barcode);
			return new Material_DTO(materialDal);
		}
		
		public IEnumerable<Material_DTO> GetMaterialsByBarcodes(List<string> barcodes)
		{
			IEnumerable<Material_DAL> materialDals = _materialRepository.GetMaterialsByBarcodes(barcodes);
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDals)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}

		public Material_DTO AddMaterial(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
			return new Material_DTO(insertedMaterial);
		}

		public IEnumerable<Material_DTO> AddMaterials(IEnumerable<Material_DTO> materialDtos)
		{
			List<Material_DTO> insertedMaterials = new List<Material_DTO>();
			foreach (Material_DTO materialDto in materialDtos)
			{
				Material_BLL materialBll = materialDto.ToBLL();
				Material_DAL materialDal = new Material_DAL(materialBll);
				Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
				insertedMaterials.Add(new Material_DTO(insertedMaterial));
			}
			return insertedMaterials;
		}
		
		public IEnumerable<Material_DTO> PrepareVehicle(int vehicleId, List<string> barcodes)
		{
			IEnumerable<Material_DAL> materialsToStore = _materialRepository.GetMaterialsByVehicleId(vehicleId);
			
			foreach (Material_DAL material in materialsToStore)
			{
				Material_DTO materialDto = new Material_DTO(material);
				Material_BLL materialBll = materialDto.ToBLL();
				materialBll.PutInStorage();
				Material_DAL materialDal = new Material_DAL(materialBll);
				_materialRepository.Update(materialDal);
			}
    
			IEnumerable<Material_DAL> materialsToPrepare = _materialRepository.GetMaterialsByBarcodes(barcodes);
			List<Material_DTO> materialDtos = new List<Material_DTO>();
			foreach (Material_DAL material in materialsToPrepare)
			{
				Material_DTO materialDto = new Material_DTO(material);
				Material_BLL materialBll = materialDto.ToBLL();
				materialBll.PutInVehicle(vehicleId);
				Material_DAL materialDal = new Material_DAL(materialBll);
				Material_DAL updatedMaterial = _materialRepository.Update(materialDal);
				materialDtos.Add(new Material_DTO(updatedMaterial));
			}
			
			return materialDtos;
		}
		
		public Material_DTO Update(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			Material_DAL updatedMaterial = _materialRepository.Update(materialDal);
			return new Material_DTO(updatedMaterial);
		}
		
		public void Delete(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			_materialRepository.Delete(materialDal);
		}

public IEnumerable<Material_DTO> HandleInterventionReturn(int vehicleId, InterventionReturn_DTO interventionReturnDto)
{
    IEnumerable<Material_DAL> materialsOnVehicle = _materialRepository.GetMaterialsByVehicleId(vehicleId);
    List<Material_DTO> updatedMaterials = new List<Material_DTO>();

    // Fetch the vehicle and find the associated intervention
    Vehicle_DAL vehicle = _vehicleRepository.GetVehicleById(vehicleId);
    int interventionId = _vehicleInterventionRepository.GetInterventionIdByVehicleId(vehicle.Id);

    foreach (Material_DAL material in materialsOnVehicle)
    {
        Material_DTO materialDto = new Material_DTO(material);
        Material_BLL materialBll = materialDto.ToBLL();
        
        if (interventionReturnDto.UsedBarcodes.Contains(materialBll.Barcode) || interventionReturnDto.UnusedBarcodes.Contains(materialBll.Barcode))
        {
            // Update usage count if material is in the used barcodes list
            if (interventionReturnDto.UsedBarcodes.Contains(materialBll.Barcode))
            {
                materialBll.UpdateUsageCount();
                
                // Create a new instance of MaterialUsageHistory
                MaterialUsageHistory_DTO usageHistory = new MaterialUsageHistory_DTO()
                {
                    MaterialId = material.Id,
                    InterventionId = interventionId, // Use the fetched interventionId
                    UsageDate = DateTime.UtcNow // Use the current date and time
                };

                // Add the usage history to the database
                _materialUsageHistoryRepository.AddUsageHistory(usageHistory);
            }
            
            // Evaluate if the material should be removed
            materialBll.ValidateUsability();
        }
        else
        {
            // Mark material as lost
            materialBll.HasBeenLost();
            
            // Add a lost record to MaterialUsageHistory
            MaterialUsageHistory_DTO lostHistory = new MaterialUsageHistory_DTO()
            {
                MaterialId = material.Id,
                InterventionId = interventionId, // Use the fetched interventionId
                UsageDate = DateTime.UtcNow, // Use the current date and time
                IsUsed = false, // Material was not used
                IsLost = true // Material is lost
            };

            // Add the lost history to the database
            _materialUsageHistoryRepository.AddUsageHistory(lostHistory);
        }

        // Update Material in the database and add it to the updated materials list
        Material_DAL updatedMaterialDal = new Material_DAL(materialBll);
        Material_DAL updatedMaterial = _materialRepository.Update(updatedMaterialDal);
        updatedMaterials.Add(new Material_DTO(updatedMaterial));
    }

    return updatedMaterials;
}

	}
}

