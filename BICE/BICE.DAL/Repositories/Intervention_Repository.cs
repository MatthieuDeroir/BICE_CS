using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL;
using BICE.BLL;
using BICE.DAL.Repositories;
using BICE.DAL.Wrappers;

namespace BICE.DAL
{
    public class Intervention_Repository : Repository<Intervention_DAL>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IDbCommandWrapper _command;
        
        public Intervention_Repository(IDbConnectionWrapper connection, IDbCommandWrapper command) : base(connection, command)
        {
            _connection = connection;
            _command = command;
        }
        public override IEnumerable<Intervention_DAL> GetAll()
        {
            var query = "SELECT * FROM Interventions";

            _connection.Open();
            _command.CommandText = query;

            using (var reader = _command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var denomination = reader["denomination"] == DBNull.Value ? (string)null : (string)reader["denomination"];
                    var description = reader["description"] == DBNull.Value ? (string)null : (string)reader["description"];
                    var startDate = (DateTime)reader["startDate"];
                    var endDate = reader["endDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["endDate"];

                    Console.WriteLine($"id: {id}, denomination: {denomination}, description: {description}, startDate: {startDate}, endDate: {endDate}");

                    yield return new Intervention_DAL(id, denomination, description, startDate, endDate);
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
    }
}
