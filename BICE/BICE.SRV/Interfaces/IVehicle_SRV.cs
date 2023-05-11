using BICE.DTO;

namespace BICE.SRV.Interfaces;

public interface IVehicle_SRV
{
    // GET
    IEnumerable<Vehicle_DTO> GetVehicle();
    Vehicle_DTO GetVehicleById(int id);
    
    IEnumerable<Vehicle_DTO> GetVehiclesByInterventionId(int id);
    
    Vehicle_DTO GetVehicleByInternalNumber(string internalNumber);
    
    // POST
    
    Vehicle_DTO AddVehicle(Vehicle_DTO vehicleDto);
    
    // PUT
    
    Vehicle_DTO Update(Vehicle_DTO vehicleDto);
    
    // DELETE
    
    void Delete(Vehicle_DTO vehicleDto);
    
    
    
}