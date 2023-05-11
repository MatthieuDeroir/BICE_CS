using System;
using System.Collections;
using BICE.DAL;
using BICE.DTO;
using BICE.BLL;

namespace BICE.SRV
{
	public class Material_SRV
	{
		private readonly Intervention_Repository _interventionRepository;
		private readonly Material_Repository _materialRepository;
		private readonly Vehicle_Repository _vehicleRepository;
		public Material_SRV()
		{
			_interventionRepository = new Intervention_Repository();
			_materialRepository = new Material_Repository();
			_vehicleRepository = new Vehicle_Repository();
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
				try
				{
					Material_DAL materialDal = new Material_DAL(materialDto.ToBLL());
					Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
					insertedMaterials.Add(new Material_DTO(insertedMaterial));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
				
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
			Material_DAL updatedMaterial = _materialRepository.Update(materialDto.ToDAL());
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
			Vehicle_DAL vehicle = _vehicleRepository.GetById(vehicleId);
			// Fetch the last intervention id for the vehicle
			int interventionId = _interventionRepository.GetLastInterventionIdByVehicleId(vehicleId);
			
			// Get the vehicleintervention id from the intervention id and the vehicle id
			int VehicleinterventionId = _vehicleRepository.GetVehicleInterventionIdByInterventionIdAndVehicleId(interventionId, vehicleId);
			

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
						MaterialUsageHistory_DAL usageHistory = new MaterialUsageHistory_DAL
						(
							material.Id,
							VehicleinterventionId, // Use the fetched vehicleinterventionId
							// Use the date and time from the intervention id
							DateTime.Now, 
							true, // Material was used
							false // Material was not lost
						);

						// Add the usage history to the database
						_materialRepository.AddUsageHistory(usageHistory);
					}
            
					// Evaluate if the material should be removed
					materialBll.ValidateUsability(); 
				}
				else
				{
					// Mark material as lost
					materialBll.HasBeenLost();
            
					// Add a lost record to MaterialUsageHistory
					MaterialUsageHistory_DAL lostHistory = new MaterialUsageHistory_DAL
					(
					material.Id,
					interventionId, // Use the fetched interventionId
					DateTime.UtcNow, // Use the current date and time
					false, // Material was not used
					true // Material is lost
					);

					// Add the lost history to the database
					_materialRepository.AddUsageHistory(lostHistory);
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

