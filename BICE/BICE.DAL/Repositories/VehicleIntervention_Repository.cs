using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL.Repositories;
using BICE.BLL;

namespace BICE.DAL
{
    public class VehicleIntervention_Repository : Repository<VehicleIntervention_DAL>
    {
        // Implement the CRUD methods for VehicleIntervention

        public override VehicleIntervention_DAL GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<VehicleIntervention_DAL> GetAll()
        {
            throw new NotImplementedException();
        }

        public override VehicleIntervention_DAL Insert(VehicleIntervention_DAL vehicleIntervention)
        {
            throw new NotImplementedException();
        }

        public override VehicleIntervention_DAL Update(VehicleIntervention_DAL vehicleIntervention)
        {
            throw new NotImplementedException();
        }

        public override void Delete(VehicleIntervention_DAL vehicleIntervention)
        {
            throw new NotImplementedException();
        }
    }
}