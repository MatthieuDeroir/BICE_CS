using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using BICE.DAL;
using BICE.DAL.Wrappers;


namespace BICE.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public string ConnectionString { get; set; }
        protected IDbConnectionWrapper Connection { get; set; }
        protected IDbCommandWrapper Command { get; set; }

        public Repository(IDbConnectionWrapper connection, IDbCommandWrapper command)
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile("appsettings.json", false, true).Build();
            ConnectionString = config.GetSection("ConnectionStrings:default").Value;

            Connection = connection;
            Command = command;

            Console.WriteLine($"ConnectionString: {ConnectionString}");
        }

        protected void InitializeConnectionAndCommand()
        {
            Connection.Open();
            Command = Connection.CreateCommand();
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