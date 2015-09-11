using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appServer.Models
{
    public class Request
    {
        public int id { get; set; }
        
        [Required]
        [ForeignKey("owner")]
        public string ownerId { get; set; }
        public virtual ApplicationUser owner { get; set; }

        [ForeignKey("fromCity")]
        public int? fromCityId { get; set; }
        public virtual City fromCity { get; set; }

        [Required]
        [ForeignKey("fromCountry")]
        public int fromCountryId { get; set; }
        public virtual Country fromCountry { get; set; }

        [ForeignKey("toCity")]
        public int? toCityId { get; set; }
        public virtual City toCity { get; set; }

        [Required]
        [ForeignKey("toCountry")]
        public int toCountryId { get; set; }
        public virtual Country toCountry { get; set; }

        public ItemType itemType { get; set; }

        [Required]
        public Decimal price { get; set; }

        [ForeignKey("currency")]
        public int currencyId { get; set; }
        public Currency currency { get; set; }

        public Decimal? estimatedVolume { get; set; }
        public Decimal estimatedWeight { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }
        public string explanation { get; set; }
    }

    public class RequestDTO
    {
        public int id { get; set; }
        public string ownerId { get; set; }
        public string UserName { get; set; }
        public string firstName { get; set; }
        public byte[] photo { get; set; }
        public City fromCity { get; set; }
        public Country fromCountry { get; set; }
        public City toCity { get; set; }
        public Country toCountry { get; set; }
        //public ItemType itemType { get; set; }
        public Decimal price { get; set; }
        public Currency currency { get; set; }
        //public Decimal? estimatedVolume { get; set; }
        public Decimal estimatedWeight { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }
    }

    public class NewRequestDTO
    {
        public int? fromCity { get; set; }
        public int fromCountry { get; set; }
        public int? toCity { get; set; }
        public int toCountry { get; set; }
        public ItemType itemType { get; set; }
        public Decimal price { get; set; }
        public int currency { get; set; }
        public Decimal? estimatedVolume { get; set; }
        public Decimal estimatedWeight { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }
    }
}
