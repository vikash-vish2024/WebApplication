using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        private static List<Country> countrylist= new List<Country>
        {
            new Country { ID = 1, CountryName = "INDIA", Capital = "NEW-DELHI" },
            new Country { ID = 2, CountryName = "NEPAL", Capital = "KATHMANDU" },
            new Country { ID = 3, CountryName = "USA", Capital = "WASHINGTON DC" },
            new Country { ID = 4, CountryName = "PAKISTAN", Capital = "KARACHI" },
        };
        //getting all countries
        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> Get()
        {
            return countrylist;
        }
        //getting all countries using responseMassage
        [HttpGet]
        [Route("ByMassage")]
        public HttpResponseMessage GetAllCountry()
        {
            return Request.CreateResponse(HttpStatusCode.OK, countrylist);
        }
        //get country by ID
        [HttpGet]
        [Route("ById/{Cid}")]
        public IHttpActionResult GetPersonById(int Cid)
        {
            var country = countrylist.Find(c => c.ID == Cid);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        //GetCountry Name by ID
        [HttpGet]
        [Route("GetName/{Cid}")]
        public IHttpActionResult GetPersonByName(int Cid)
        {
            string countryname = countrylist.Where(c => c.ID == Cid).Select(c => c.CountryName).FirstOrDefault();
            if (countryname == null)
            {
                return NotFound();
            }
            return Ok(countryname);
        }

        //create/add new country
        [HttpPost]
        [Route("AllPost")]
        public List<Country> PostAll([FromBody] Country country)
        {
            countrylist.Add(country);
            return countrylist;
        }
        [HttpPost]
        [Route("CountryPost")]
        public void PersonPost([FromUri] int ID, string CountryName, string Capital)
        {
            Country country = new Country();
            country.ID = ID;
            country.CountryName = CountryName;
            country.Capital = Capital;
            countrylist.Add(country);
        }

        //update country
        [HttpPut]
        [Route("UpdateCountry")]
        public void Put(int cid, [FromUri] Country country)
        {
            countrylist[cid - 1] = country;
        }

        //delete country
        [HttpDelete]
        [Route("DelCountry")]
        public void Delete(int cid)
        {
            countrylist.RemoveAt(cid - 1);
        }
    }
}
