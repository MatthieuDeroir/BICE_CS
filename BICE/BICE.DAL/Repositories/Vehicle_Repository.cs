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

        public override IEnumerable<Vehicle_DAL> GetAll()
        {
            var query = "SELECT * FROM Vehicles";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Vehicle_DAL(
                                (int)reader["id"],
                                (string)reader["denomination"],
                                (string)reader["internalNumber"],
                                (string)reader["licensePlate"],
                                (bool)reader["isActive"]
                            );
                        }
                    }
                }
            }
        }
        
        public override Vehicle_DAL GetById(int id)
        {
            var query = "SELECT * FROM Vehicles WHERE id = @id";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vehicle_DAL(
                                (int)reader["id"],
                                (string)reader["denomination"],
                                (string)reader["internalNumber"],
                                (string)reader["licensePlate"],
                                (bool)reader["isActive"]
                            );
                        }
                    }
                }
            }

            return null;
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
            var query = "UPDATE Vehicles SET internalNumber = @internalNumber, denomination = @denomination, licensePlate = @licensePlate, isActive = @isActive WHERE id = @id";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", vehicle.Id);
                    command.Parameters.AddWithValue("@internalNumber", vehicle.InternalNumber);
                    command.Parameters.AddWithValue("@denomination", vehicle.Denomination);
                    command.Parameters.AddWithValue("@licensePlate", vehicle.LicensePlate);
                    command.Parameters.AddWithValue("@isActive", vehicle.IsActive);

                    command.ExecuteNonQuery();
                }
            }

            return vehicle;
        }

        public override void Delete(Vehicle_DAL vehicle)
        {
            var query = "DELETE FROM Vehicles WHERE id = @id";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", vehicle.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}