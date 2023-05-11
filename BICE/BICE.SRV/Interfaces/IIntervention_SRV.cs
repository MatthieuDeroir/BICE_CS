using BICE.DTO;

namespace BICE.SRV
{
    public interface IIntervention_SRV
    {
        IEnumerable<Intervention_DTO> GetIntervention();
        Intervention_DTO GetInterventionById(int id);
        IEnumerable<Vehicle_DTO> GetVehiclesByInterventionId(int interventionId);
        Intervention_DTO AddIntervention(Intervention_DTO interventionDto);
        Task AddVehicleToIntervention(int interventionId, int vehicleId);
        Intervention_DTO Update(Intervention_DTO interventionDto);
        void Delete(Intervention_DTO interventionDto);
    }
}