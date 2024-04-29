using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace Asssesment_1_2.Models.Repository
{
    public class IMoviesRespository
    {
        public interface IMoviesRepository<T> where T : class 
        {
            IEnumerable<T> GetAll();
            T GetById(object Id);  
            void Insert(T obj);
            void Update(T obj);
            void Delete(Object Id);
            void Save();
        }
    }
}