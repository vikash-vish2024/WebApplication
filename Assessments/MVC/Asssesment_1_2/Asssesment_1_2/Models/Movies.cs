using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Asssesment_1_2.Models
{
    public class Movies
    {
        [Key]
        public int Mid { get; set; }
        public string MovieName { get; set; }
        public DateTime DateOfRelease { get; set; }
    }
}