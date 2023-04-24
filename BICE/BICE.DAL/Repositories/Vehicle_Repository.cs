using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL.Repositories;
using BICE.BLL;

namespace BICE.DAL
{
    public class Vehicle_Repository : Repository<Vehicle_DAL>
    {
        // Implement the CRUD methods for Vehicle

        public override Vehicle_DAL GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Vehicle_DAL> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Vehicle_DAL Insert(Vehicle_DAL vehicle)
        {
            var query = "INSERT INTO Vehicles (internalNumber, denomination, licensePlate, isActive) OUTPUT INSERTED.id VALUES (@internalNumber, @denomination, @licensePlate, @isActive)";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@internalNumber", vehicle.InternalNumber);
                    command.Parameters.AddWithValue("@denomination", vehicle.Denomination);
                    command.Parameters.AddWithValue("@licensePlate", vehicle.LicensePlate);
                    command.Parameters.AddWithValue("@isActive", vehicle.IsActive);

                    int id = (int)command.ExecuteScalar();
                    vehicle.Id = id;
                }
            }

            return vehicle;
        }

        public override Vehicle_DAL Update(Vehicle_DAL vehicle)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Vehicle_DAL vehicle)
        {
            throw new NotImplementedException();
        }
    }
}