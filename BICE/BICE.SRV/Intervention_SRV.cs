using BICE.DTO;
using BICE.BLL;
using BICE.DAL;

namespace BICE.SRV
{
	public class Intervention_SRV
	{
		private readonly Intervention_Repository _interventionRepository;
		public Intervention_SRV()
		{
			_interventionRepository = new Intervention_Repository();
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

