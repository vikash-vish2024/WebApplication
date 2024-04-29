using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Asssesment_1_2.Models
{
    public class MoviesContext :DbContext
    {
        public MoviesContext() : base("MoviesConnection")
        { }
        public DbSet<Movies> movies { get; set; }
    }
}
