using BICE.DTO;
using BICE.BLL;
using BICE.DAL;

namespace BICE.SRV
{
	public class Intervention_SRV
	{
		private readonly Intervention_Repository _interventionRepository;
		private readonly Material_Repository _materialRepository;
		private readonly Vehicle_Repository _vehicleRepository;
		public Intervention_SRV()
		{
			_interventionRepository = new Intervention_Repository();
			_materialRepository = new Material_Repository();
			_vehicleRepository = new Vehicle_Repository();
		}
		
		public IEnumerable<Intervention_DTO> GetIntervention()
		{
			IEnumerable<Intervention_DAL> interventionDal = _interventionRepository.GetAll();
			List<Intervention_DTO> interventionDto = new List<Intervention_DTO>();
			foreach (Intervention_DAL intervention in interventionDal)
			{
				interventionDto.Add(new Intervention_DTO(intervention));
			}
			return interventionDto;
		}
		
		public Intervention_DTO GetInterventionById(int id)
		{
			Intervention_DAL interventionDal = _interventionRepository.GetById(id);
			return new Intervention_DTO(interventionDal);
		}
		
		public IEnumerable<Vehicle_DTO> GetVehiclesByInterventionId(int interventionId)
		{
			IEnumerable<Vehicle_DAL> vehicleDal = _interventionRepository.GetVehiclesByInterventionId(interventionId);
			List<Vehicle_DTO> vehicleDto = new List<Vehicle_DTO>();
			foreach (Vehicle_DAL vehicle in vehicleDal)
			{
				vehicleDto.Add(new Vehicle_DTO(vehicle));
			}
			return vehicleDto;
		}
		
		public Intervention_DTO AddIntervention(Intervention_DTO interventionDto)
		{
			Intervention_BLL interventionBll = interventionDto.ToBLL();
			Intervention_DAL interventionDal = new Intervention_DAL(interventionBll);
			Intervention_DAL insertedIntervention = _interventionRepository.Insert(interventionDal);
			return new Intervention_DTO(insertedIntervention);
		}

		public Task AddVehicleToIntervention(int interventionId, int vehicleId)
		{
			_interventionRepository.AddVehicleToIntervention(interventionId, vehicleId);
			return Task.CompletedTask;
		}
		
		public Intervention_DTO Update(Intervention_DTO interventionDto)
		{
			Intervention_BLL interventionBll = interventionDto.ToBLL();
			Intervention_DAL interventionDal = new Intervention_DAL(interventionBll);
			Intervention_DAL updatedIntervention = _interventionRepository.Update(interventionDal);
			return new Intervention_DTO(updatedIntervention);
		}
		
		public void Delete(Intervention_DTO interventionDto)
		{
			Intervention_BLL interventionBll = interventionDto.ToBLL();
			Intervention_DAL interventionDal = new Intervention_DAL(interventionBll);
			_interventionRepository.Delete(interventionDal);
		}
		
		public IEnumerable<Material_DTO> HandleInterventionReturn(int vehicleId, InterventionReturn_DTO interventionReturnDto)
		{
			IEnumerable<Material_DAL> materialsOnVehicle = _materialRepository.GetMaterialsByVehicleId(vehicleId);
			List<Material_DTO> updatedMaterials = new List<Material_DTO>();

			// Fetch the vehicle and find the associated intervention
			Vehicle_DAL vehicle = _vehicleRepository.GetById(vehicleId);
			// Fetch the last intervention id for the vehicle
			int interventionId = _interventionRepository.GetLastInterventionIdByVehicleId(vehicleId);

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
							interventionId, // Use the fetched interventionId
							// Use the date and time from the intervention id
							_interventionRepository.GetById(interventionId).EndDate,
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

