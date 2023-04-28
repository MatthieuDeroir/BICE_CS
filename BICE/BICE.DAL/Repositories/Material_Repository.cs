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
                        (int?)reader["MaxUsageCount"],
                        (DateTime?)reader["ExpirationDate"],
                        (DateTime?)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsUsable"]
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
                        (int?)reader["MaxUsageCount"],
                        (DateTime?)reader["ExpirationDate"],
                        (DateTime?)reader["NextControlDate"],
                        (bool)reader["IsStored"],
                        (bool)reader["IsLost"],
                        (bool)reader["IsUsable"]
                    ));
                }
            }
            return materials;
        }

        public override Material_DAL Insert(Material_DAL material)
        {
            var query = "INSERT INTO Materials (Denomination, Barcode, Category, UsageCount, MaxUsageCount, ExpirationDate, NextControlDate, IsStored, IsLost, IsUsable) VALUES (@Denomination, @Barcode, @Category, @UsageCount, @MaxUsageCount, @ExpirationDate, @NextControlDate, @IsStored, @IsLost, @IsUsable)";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Denomination", material.Denomination);
                command.Parameters.AddWithValue("@Barcode", material.Barcode);
                command.Parameters.AddWithValue("@Category", material.Category);
                command.Parameters.AddWithValue("@UsageCount", material.UsageCount);
                command.Parameters.AddWithValue("@MaxUsageCount", material.MaxUsageCount);
                command.Parameters.AddWithValue("@ExpirationDate", material.ExpirationDate);
                command.Parameters.AddWithValue("@NextControlDate", material.NextControlDate);
                command.Parameters.AddWithValue("@IsStored", material.IsStored);
                command.Parameters.AddWithValue("@IsLost", material.IsLost);
                command.Parameters.AddWithValue("@IsUsable", material.IsUsable);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return material;
        }

        public override Material_DAL Update(Material_DAL material)
        {
            var query = "UPDATE Materials SET Denomination = @Denomination, Barcode = @Barcode, Category = @Category, UsageCount = @UsageCount, MaxUsageCount = @MaxUsageCount, ExpirationDate = @ExpirationDate, NextControlDate = @NextControlDate, IsStored = @IsStored, IsLost = @IsLost, IsUsable = @IsUsable WHERE Id = @Id";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", material.Id);
                command.Parameters.AddWithValue("@Denomination", material.Denomination);
                command.Parameters.AddWithValue("@Barcode", material.Barcode);
                command.Parameters.AddWithValue("@Category", material.Category);
                command.Parameters.AddWithValue("@UsageCount", material.UsageCount);
                command.Parameters.AddWithValue("@MaxUsageCount", material.MaxUsageCount);
                command.Parameters.AddWithValue("@ExpirationDate", material.ExpirationDate);
                command.Parameters.AddWithValue("@NextControlDate", material.NextControlDate);
                command.Parameters.AddWithValue("@IsStored", material.IsStored);
                command.Parameters.AddWithValue("@IsLost", material.IsLost);
                command.Parameters.AddWithValue("@IsUsable", material.IsUsable);
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
    }
}