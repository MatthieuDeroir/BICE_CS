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
            throw new NotImplementedException();
        }

        public override IEnumerable<Material_DAL> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Material_DAL Insert(Material_DAL material)
        {
            throw new NotImplementedException();
        }

        public override Material_DAL Update(Material_DAL material)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Material_DAL material)
        {
            throw new NotImplementedException();
        }
    }
}