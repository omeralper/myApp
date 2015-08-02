using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appServer.Models
{
    public class Requestor
    {
        public int id { get; set; }
        //[ForeignKey("owner")]
        //public string ownerId { get; set; }
        //public virtual ApplicationUser owner { get; set; }
        //omer:internetten hızlıca bulduğum elimdeki data'larda bunlar string. ondan böyle
        public string fromCity { get; set; }
        public string fromCountry { get; set; }
        public string toCity { get; set; }
        public string toCountry { get; set; }
         
        //omer:açıklama varken bu gerekli mi bilmiyorum
        public string itemName { get; set; }
          
        public ItemType itemType { get; set; }

        public Decimal price { get; set; }

        public Decimal estimatedVolume { get; set; }
        public Decimal estimatedWeight { get; set; }

        public string explanation { get; set; }

    }
}
