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
            var query = "SELECT * FROM Materials WHERE Id_Vehicle = @VehicleId";
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
            var parameters = new List<SqlParameter>();
            var paramNames = barcodes.Select((s, i) => "@Barcode" + i.ToString()).ToList();

            for (int i = 0; i < barcodes.Count; i++)
            {
                parameters.Add(new SqlParameter(paramNames[i], barcodes[i]));
            }

            var query = "SELECT * FROM Materials WHERE Barcode IN (" + string.Join(", ", paramNames) + ")";

            var materials = new List<Material_DAL>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());
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
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return material;
        }

        public override Material_DAL Update(Material_DAL material)
        {
            var query = "UPDATE Materials SET Denomination = @Denomination, Barcode = @Barcode, Category = @Category, UsageCount = @UsageCount, MaxUsageCount = @MaxUsageCount, ExpirationDate = @ExpirationDate, NextControlDate = @NextControlDate, IsStored = @IsStored, IsLost = @IsLost, IsRemoved = @IsRemoved, Id_Vehicle = @VehicleId WHERE Id = @Id";
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
            var query = "SELECT * FROM Materials WHERE Id_Vehicle = @VehicleId";
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
        
        
        public void AddUsageHistory(MaterialUsageHistory_DAL usageHistory)
        {
            var query = "INSERT INTO MaterialUsageHistory (Id_Material, Id_Vehicle_Intervention, UsageDate, IsUsed, IsLost) VALUES (@Id_Material, @Id_Vehicle_Intervention, @UsageDate, @IsUsed, @IsLost)";
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id_Material", usageHistory.MaterialId);
                command.Parameters.AddWithValue("@Id_Vehicle_Intervention", usageHistory.VehicleInterventionId);
                command.Parameters.AddWithValue("@UsageDate", usageHistory.UsageDate);
                command.Parameters.AddWithValue("@IsUsed", usageHistory.IsUsed);
                command.Parameters.AddWithValue("@IsLost", usageHistory.IsLost);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<MaterialUsageHistory_DAL> GetUsageHistoryByMaterial(int materialId)
        {
            var query = "SELECT * FROM MaterialUsageHistory WHERE Id_Material = @Id_Material";
            
            var usageHistory = new List<MaterialUsageHistory_DAL>();
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id_Material", materialId);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usageHistory.Add(new MaterialUsageHistory_DAL(
                        (int)reader["Id"],
                        (int)reader["Id_Material"],
                        (int)reader["Id_Vehicle_Intervention"],
                        (DateTime)reader["UsageDate"],
                        (bool)reader["IsUsed"],
                        (bool)reader["IsLost"]
                    ));
                }
            }
            return usageHistory;
        }
    }
}