using System.Collections.Generic;
using BICE.DTO;
using BICE.BLL;
using BICE.DAL;

namespace BICE.SRV
{
    public class Vehicle_SRV
    {
        private readonly Vehicle_Repository _vehicleRepository;

        public Vehicle_SRV()
        {
            _vehicleRepository = new Vehicle_Repository();
        }

        public Vehicle_DTO AddVehicle(Vehicle_DTO vehicleDto)
        {
            Vehicle_BLL vehicleBll = vehicleDto.ToBLL();
            Vehicle_DAL vehicleDal = new Vehicle_DAL(vehicleBll);
            Vehicle_DAL insertedVehicle = _vehicleRepository.Insert(vehicleDal);
            return new Vehicle_DTO(insertedVehicle);
        }
        
        public IEnumerable<Vehicle_DTO> GetVehicle()
        {
            IEnumerable<Vehicle_DAL> vehicleDal = _vehicleRepository.GetAll();
            List<Vehicle_DTO> vehicleDto = new List<Vehicle_DTO>();
            foreach (Vehicle_DAL vehicle in vehicleDal)
            {
                vehicleDto.Add(new Vehicle_DTO(vehicle));
            }
            return vehicleDto;
        }

        // Implement other CRUD methods using DTO conversions
    }
}