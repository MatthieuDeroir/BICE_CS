using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL.Repositories;

namespace BICE.DAL;

public class VehicleMaterial_Repository : Repository<VehicleMaterial_DAL>
{
    
    
    public override VehicleMaterial_DAL GetById(int id)
    {
        var query = "SELECT * FROM VehicleMaterials WHERE id = @id";

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
                        return new VehicleMaterial_DAL(
                            (int)reader["id"],
                            (int)reader["vehicleId"],
                            (int)reader["materialId"]);
                    }
                }
            }
        }
        return null;
    }
    
    public VehicleMaterial_DAL GetByVehicleId(int vehicleId)
    {
        var query = "SELECT * FROM VehicleMaterials WHERE vehicleId = @vehicleId";

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
                        return new VehicleMaterial_DAL(
                            (int)reader["id"],
                            (int)reader["vehicleId"],
                            (int)reader["materialId"]);
                    }
                }
            }
        }
        return null;
    }
    
    public override IEnumerable<VehicleMaterial_DAL> GetAll()
    {
        var query = "SELECT * FROM VehicleMaterials";
        var materials = new List<VehicleMaterial_DAL>();
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand(query, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                materials.Add(new VehicleMaterial_DAL(
                    (int)reader["id"],
                    (int)reader["vehicleId"],
                    (int)reader["materialId"]));
            }
        }
        return materials;
    }
    
    public override VehicleMaterial_DAL Insert(VehicleMaterial_DAL vehicleMaterial)
    {
        var query ="INSERT INTO VehicleMaterials (vehicleId, materialId) VALUES (@vehicleId, @materialId)";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vehicleId", vehicleMaterial.VehicleId);
            command.Parameters.AddWithValue("@materialId", vehicleMaterial.MaterialId);
            command.ExecuteNonQuery();
        }
        return vehicleMaterial;
    }
    
    public override VehicleMaterial_DAL Update(VehicleMaterial_DAL vehicleMaterial)
    {
        var query = "UPDATE VehicleMaterials SET vehicleId = @vehicleId, materialId = @materialId WHERE id = @id";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vehicleId", vehicleMaterial.VehicleId);
            command.Parameters.AddWithValue("@materialId", vehicleMaterial.MaterialId);
            command.Parameters.AddWithValue("@id", vehicleMaterial.Id);
            command.ExecuteNonQuery();
        }
        return vehicleMaterial;
    }
    
    public override void Delete(VehicleMaterial_DAL vehicleMaterial)
    {
        var query = "DELETE FROM VehicleMaterials WHERE id = @id";
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", vehicleMaterial.Id);
            command.ExecuteNonQuery();
        }
    }
}