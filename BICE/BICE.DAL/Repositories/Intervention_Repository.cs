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
        public override Intervention_DAL GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Intervention_DAL Update(Intervention_DAL entity)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Intervention_DAL> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Intervention_DAL Insert(Intervention_DAL intervention)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Intervention_DAL intervention)
        {
            throw new NotImplementedException();
        }
        
    }
}
