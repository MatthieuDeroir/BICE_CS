using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BICE.DAL.Repositories;
using BICE.BLL;


namespace BICE.DAL
{
    public class Material_Repository : Repository<Material_DAL>
    {
        // Implement the CRUD methods for Material

        public override Material_DAL GetById(int id)
        {
            var query = "SELECT * FROM Materials WHERE Id = @Id";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    );

                }
            }
            return null;
        }

        public override IEnumerable<Material_DAL> GetAll()
        {
            var query = "SELECT * FROM Materials";
            var materials = new List<Material_DAL>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    ));
                }
            }
            return materials;
        }

        public IEnumerable<Material_DAL> GetByVehicleId(int id)
        {
            var query = "SELECT * FROM Materials WHERE VehicleId = @VehicleId";
            var materials = new List<Material_DAL>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    ));
                }
            }
            return materials;
        }
        
        public Material_DAL GetByBarcode(string barcode)
        {
            var query = "SELECT * FROM Materials WHERE Barcode = @Barcode";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Barcode", barcode);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    );
                }
            }
            return null;
        }
        
        public IEnumerable<Material_DAL> GetMaterialsByBarcodes(List<string> barcodes)
        {
            var query = "SELECT * FROM Materials WHERE Barcode IN @Barcodes";
            var materials = new List<Material_DAL>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Barcodes", barcodes);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    ));
                }
            }
            return materials;
        }

        public override Material_DAL Insert(Material_DAL material)
        {
            var query = "INSERT INTO Materials (Denomination, Barcode, Category, UsageCount, MaxUsageCount, ExpirationDate, NextControlDate, IsStored, IsLost, IsRemoved, Id_Vehicle) VALUES (@Denomination, @Barcode, @Category, @UsageCount, @MaxUsageCount, @ExpirationDate, @NextControlDate, @IsStored, @IsLost, @IsRemoved, @VehicleId)";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Denomination", material.Denomination);
                command.Parameters.AddWithValue("@Barcode", material.Barcode);
                command.Parameters.AddWithValue("@Category", material.Category);
                command.Parameters.AddWithValue("@UsageCount", material.UsageCount);
                command.Parameters.AddWithValue("@MaxUsageCount", material.MaxUsageCount.HasValue ? (object)material.MaxUsageCount.Value : DBNull.Value);
                command.Parameters.AddWithValue("@ExpirationDate", material.ExpirationDate.HasValue ? (object)material.ExpirationDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@NextControlDate", material.NextControlDate.HasValue ? (object)material.NextControlDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@IsStored", true);
                command.Parameters.AddWithValue("@IsLost", false);
                command.Parameters.AddWithValue("@IsRemoved", false);
                command.Parameters.AddWithValue("@VehicleId", material.VehicleId.HasValue ? (object)material.VehicleId.Value : DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return material;
        }

        public override Material_DAL Update(Material_DAL material)
        {
            var query = "UPDATE Materials SET Denomination = @Denomination, Barcode = @Barcode, Category = @Category, UsageCount = @UsageCount, MaxUsageCount = @MaxUsageCount, ExpirationDate = @ExpirationDate, NextControlDate = @NextControlDate, IsStored = @IsStored, IsLost = @IsLost, IsRemoved = @IsRemoved, Id_Vehicle = @VehicleId, WHERE Id = @Id";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", material.Id);
                command.Parameters.AddWithValue("@Denomination", material.Denomination);
                command.Parameters.AddWithValue("@Barcode", material.Barcode);
                command.Parameters.AddWithValue("@Category", material.Category);
                command.Parameters.AddWithValue("@UsageCount", material.UsageCount);
                command.Parameters.AddWithValue("@MaxUsageCount", material.MaxUsageCount.HasValue ? (object)material.MaxUsageCount.Value : DBNull.Value);
                command.Parameters.AddWithValue("@ExpirationDate", material.ExpirationDate.HasValue ? (object)material.ExpirationDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@NextControlDate", material.NextControlDate.HasValue ? (object)material.NextControlDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@IsStored", material.IsStored);
                command.Parameters.AddWithValue("@IsLost", material.IsLost);
                command.Parameters.AddWithValue("@IsRemoved", material.IsRemoved);
                command.Parameters.AddWithValue("@VehicleId", material.VehicleId.HasValue ? (object)material.VehicleId.Value : DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return material;
        }

        public override void Delete(Material_DAL material)
        {
            var query = "DELETE FROM Materials WHERE Id = @Id";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", material.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Material_DAL> GetMaterialsByVehicleId(int vehicleId)
        {
            var query = "SELECT * FROM Materials WHERE VehicleId = @VehicleId";
            var materials = new List<Material_DAL>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleId", vehicleId);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(new Material_DAL(
                        (int)reader["Id"],
                        (string)reader["Denomination"],
                        (string)reader["Barcode"],
                        (string)reader["Category"],
                        (int)reader["UsageCount"],
                        reader["MaxUsageCount"] == DBNull.Value ? (int?)null : (int)reader["MaxUsageCount"],
                        reader["ExpirationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpirationDate"],
                        reader["NextControlDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsRemoved"],
                        reader["Id_Vehicle"] == DBNull.Value ? (int?)null : (int)reader["Id_Vehicle"]
                    ));
                }
            }
            return materials;
        }
    }
}