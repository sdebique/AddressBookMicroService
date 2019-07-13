using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using AddressBookMicroService.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AddressBookMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualController : ControllerBase
    {
        string addressInfoPath = @".\individualData.json";

        // GET: api/Individual
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            List<string> tempstringvals = new List<string>();
            Dictionary<string, List<Individual>> sl = new Dictionary<string, List<Individual>>();

            string[] keyresult = new string[3];
            string[] valueresult = new string[3];
            string[] sortedData = new string[18];
            ;
            MultipleIndividuals individuals = new MultipleIndividuals();            

            JsonSerializer js = new JsonSerializer();

            if (System.IO.File.Exists(addressInfoPath))
            {
                using (StreamReader reader = new StreamReader(addressInfoPath))
                {
                    string json = reader.ReadToEnd();
                    MultipleIndividuals tempIndsData = JsonConvert.DeserializeObject<MultipleIndividuals>(json);

                    var temp = tempIndsData.Individual.GroupBy(c => c.City).ToDictionary(cg=>cg.Key, cg=>cg.ToList());
                    foreach (var tempItem in temp)
                    {
                        sl.Add(tempItem.Key, tempItem.Value);
                    }
                }
                sl.Keys.CopyTo(keyresult, 0);

                
                foreach(var val in sl.Values)
                {
                    for (int i = 0; i < val.Count; i++)
                    {
                        tempstringvals.Add(val[i].FirstName);
                        tempstringvals.Add(val[i].LastName);
                        tempstringvals.Add(val[i].StreetAddress);
                        tempstringvals.Add(val[i].City);
                        tempstringvals.Add(val[i].Country);
                    }
                }
                valueresult = tempstringvals.ToArray();                
            }
            Array.Copy(keyresult, sortedData, keyresult.Length);
            Array.Copy(valueresult, 0, sortedData, keyresult.Length, valueresult.Length);

            return sortedData;
        }

        // GET: api/Individual/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Individual
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Individual/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
