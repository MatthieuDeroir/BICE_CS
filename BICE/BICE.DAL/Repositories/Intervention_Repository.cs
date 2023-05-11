using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL;
using BICE.BLL;
using BICE.DAL.Repositories;

namespace BICE.DAL
{
    public class Intervention_Repository : Repository<Intervention_DAL>
    {
        public override IEnumerable<Intervention_DAL> GetAll()
        {
            var query = "SELECT * FROM Interventions";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Intervention_DAL(
                                (int)reader["id"],
                                reader["denomination"] == DBNull.Value ? (string)null : (string)reader["denomination"],
                                reader["description"] == DBNull.Value ? (string)null : (string)reader["description"],
                                (DateTime)reader["startDate"],
                           reader["endDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["endDate"]
                            );
                        }
                    }
                }
            }
        }

        public override Intervention_DAL GetById(int id)
        {
            var query = "SELECT * FROM Interventions WHERE id = @id";
            
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
                            return new Intervention_DAL(
                                (int)reader["id"],
                                (string)reader["denomination"],
                                reader["description"] == DBNull.Value ? (string)null : (string)reader["description"],
                                (DateTime)reader["startDate"],
                                reader["endDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["endDate"]
                            );
                        }
                    }
                }
            }
            return null;
        }

        public override Intervention_DAL Update(Intervention_DAL intervention)
        {
            var query = "UPDATE Interventions SET denomination = @denomination, description = @description, startDate = @startDate, endDate = @endDate WHERE id = @id";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", intervention.Id);
                    command.Parameters.AddWithValue("@denomination", intervention.Denomination);
                    command.Parameters.AddWithValue("@description", intervention.Description);
                    command.Parameters.AddWithValue("@startDate", intervention.StartDate);
                    command.Parameters.AddWithValue("@endDate", intervention.EndDate);
                    
                    command.ExecuteNonQuery();
                }
            }
            return intervention;
        }
        
        public async Task AddVehicleToIntervention(int interventionId, int vehicleId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO VehicleIntervention (id_intervention, id_vehicle) VALUES (@interventionId, @vehicleId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@interventionId", interventionId);
                    command.Parameters.AddWithValue("@vehicleId", vehicleId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        
        public override Intervention_DAL Insert(Intervention_DAL intervention)
        {
            var query = "INSERT INTO Interventions (denomination, description, startDate, endDate) VALUES (@denomination, @description, @startDate, @endDate)";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@denomination", intervention.Denomination);
                    command.Parameters.AddWithValue("@description", intervention.Description);
                    command.Parameters.AddWithValue("@startDate", intervention.StartDate);
                    // intrvention.EndDate is nullable, so we need to check if it has a value
                    command.Parameters.AddWithValue("@endDate", intervention.EndDate.HasValue ? (object)intervention.EndDate.Value : DBNull.Value);
                    
                    command.ExecuteNonQuery();
                }
            }
            return intervention;
        }

        public override void Delete(Intervention_DAL intervention)
        {
            var query = "DELETE FROM Interventions WHERE id = @id";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", intervention.Id);
                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Vehicle_DAL> GetVehiclesByInterventionId(int interventionId)
        {
            var query = "SELECT * FROM Vehicles WHERE id IN (SELECT id_vehicle FROM VehicleIntervention WHERE id_intervention = @interventionId)";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@interventionId", interventionId);

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

        public int GetLastInterventionIdByVehicleId(int vehicleId)
        {
            var query = "SELECT TOP 1 id_intervention FROM VehicleIntervention WHERE id_vehicle = @vehicleId ORDER BY id_intervention DESC";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@vehicleId", vehicleId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (int)reader["id_intervention"];
                        }
                    }
                }
            }
            return 0;
        }
    }
}
