﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BICE.DAL;

namespace BICE.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public string ConnectionString { get; set; }
        protected SqlConnection Connection { get; set; }
        protected SqlCommand Command { get; set; }

        public Repository()
        {
            var builder = new ConfigurationBuilder();
            //var config = builder.AddJsonFile("appsettings.json", false, true).Build();
            //ConnectionString = config.GetConnectionString("default");


            // connection string for VICOOOOOOOOOOOOOO0000000000 🙂
            ConnectionString = "Data Source=localhost;Initial Catalog=BICE_DATABASE;Integrated Security=True";

            // connection string for __M_A_T_Y_W_IZOD
            //var config = builder.AddJsonFile("appsettings.json", false, true).Build();
            //ConnectionString =
            //    "Data Source=localhost,1433;Initial Catalog=BICE_DATABASE;User Id=sa;Password=78934797497xX!!;";


          
          // connection string for VICOOOOOOOOOOOOOO0000000000 :)  
             // ConnectionString = "Data Source=localhost;Integrated Security=True";

          // connection string for _M_A_A_W_O_X_
            //var config = builder.AddJsonFile("appsettings.json", false, true).Build();
            //ConnectionString =
            //    "Data Source=localhost,1433;Initial Catalog=BICE_DATABASE;User Id=sa;Password=78934797497xX!!;";
          

            Connection = new SqlConnection(ConnectionString);

            Console.WriteLine($"ConnectionString: {ConnectionString}");
        }

        protected void InitializeConnectionAndCommand()
        {
            Connection = new SqlConnection(ConnectionString);
            Command = Connection.CreateCommand();
            Connection.Open();
        }

        protected void CloseAndDisposeConnectionAndCommand()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
            if (Command != null)
                Command.Dispose();
        }

        public abstract T Insert(T entity);

        public abstract T Update(T entity);

        public abstract void Delete(T entity);

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(int id);
    }
}
