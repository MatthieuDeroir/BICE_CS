using BICE.DTO;
using BICE.BLL;
using BICE.DAL;

namespace BICE.SRV
{
	public class Intervention_SRV : IIntervention_SRV
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


	}
}

