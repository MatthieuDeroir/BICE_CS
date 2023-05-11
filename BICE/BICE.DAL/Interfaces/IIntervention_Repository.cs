namespace BICE.DAL.Repositories;

public interface IIntervention_Repository : IRepository<Intervention_DAL>
{
    /// <summary>
    /// Defines the repository operations for handling intervention data.
    /// </summary>
    public IEnumerable<Intervention_DAL> GetAll();
    
    public Intervention_DAL GetById(int id);
    
    public Intervention_DAL Insert(Intervention_DAL entity);
    
    public Intervention_DAL Update(Intervention_DAL entity);
    
    public Task AddVehicleToIntervention(int interventionId, int vehicleId);
    
    public Task RemoveVehicleFromIntervention(int interventionId, int vehicleId);

    public void Delete(Intervention_DAL intervention);

    public IEnumerable<Vehicle_DAL> GetVehiclesByInterventionId(int interventionId);


}