using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asssesment_1_2.Models;
using System.Data.Entity;
using NPOI.SS.Formula.Functions;

namespace Asssesment_1_2.Models.Repository
{
    public class T
    {
        MoviesContext db;
        DbSet<NPOI.SS.Formula.Functions.T> dbset;

        public T()
        {
            db = new MoviesContext();
            dbset = db.Set<NPOI.SS.Formula.Functions.T>();
        }
        public void Delete(Object Id)
        {
            NPOI.SS.Formula.Functions.T getmodel = dbset.Find(Id);
            dbset.Remove(getmodel);
        }

        public IEnumerable<NPOI.SS.Formula.Functions.T> GetAll()
        {
            return dbset.ToList();
        }

        public NPOI.SS.Formula.Functions.T GetById(object Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(NPOI.SS.Formula.Functions.T obj)
        {
            dbset.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(NPOI.SS.Formula.Functions.T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}