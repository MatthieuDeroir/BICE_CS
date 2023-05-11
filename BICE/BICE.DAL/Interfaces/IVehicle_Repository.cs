using System.Collections.Generic;
using BICE.DAL;

namespace BICE.DAL.Repositories
{
    public interface IVehicleRepository
    {
        /// <summary>
        /// Defines the repository operations for handling vehicle data.
        /// </summary>
        IEnumerable<Vehicle_DAL> GetAll();
        Vehicle_DAL GetById(int id);
        IEnumerable<Vehicle_DAL> GetByInterventionId(int interventionId);
        Vehicle_DAL GetByInternalNumber(string internalNumber);
        Vehicle_DAL Insert(Vehicle_DAL vehicle);
        Vehicle_DAL Update(Vehicle_DAL vehicle);
        void Delete(Vehicle_DAL vehicle);
        int GetVehicleInterventionIdByInterventionIdAndVehicleId(int interventionId, int vehicleId);
    }
}