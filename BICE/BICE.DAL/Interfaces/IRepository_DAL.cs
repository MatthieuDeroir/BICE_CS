﻿using System.Collections.Generic;

namespace BICE.DAL.Repositories
{
    /// <summary>
    /// Defines a generic repository pattern for handling basic CRUD operations.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
