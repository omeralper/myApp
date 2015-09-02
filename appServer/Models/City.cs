using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace appServer.Models
{
    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }

        [ForeignKey("country")]
        [JsonIgnore]
        public int countryId { get; set; }

        [JsonIgnore]
        public virtual Country country{ get; set; }
    }

}
