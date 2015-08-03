using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace appServer.Models
{
    public class Travel
    {
        public int id { get; set; }
        //[ForeignKey("owner")]
        //public string ownerId { get; set; }
        //public virtual ApplicationUser owner { get; set; }//test

        //omer:internetten hızlıca bulduğum elimdeki data'larda bunlar string. ondan böyle
        [Required]
        public string fromCity { get; set; }
        [Required]
        public string fromCountry { get; set; }
        [Required]
        public string toCity { get; set; }
        [Required]
        public string toCountry { get; set; }
        public decimal? availableWeight { get; set; }
        public decimal? availableVolume { get; set; }
        //omer:kesin olmayan tarihler olur diye aralık verdim.
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime finishDate { get; set; }

        //omer:belki böyle bişey ihtiyaç olur
        public ItemType? itemType { get; set; }

        //omer:belki taşıyıcı, taşımak isteyeceği ürünler için min fiyat vermek ister.
        public Decimal? price { get; set; }


        public string explanation { get; set; }
    }

    
}