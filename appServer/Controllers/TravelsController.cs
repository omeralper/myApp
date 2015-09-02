using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using appServer.Models;
using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNet.Identity;
using appServer.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace appServer.Controllers
{
    //[Authorize]
    public class TravelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Travels
        /// <summary>
        /// Bu methoda parametre geliyor aslında. Query string'ten parametre geliyor. Eğer query çok uzun olursa (max 2000 char) bu methodu get değil post yapmamız gerek. 
        /// </summary>
        /// <returns></returns>
        public IList<TravelDTO> GetTravels()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string wildCard = nvc["ticketWildCard"] == null ? "" : nvc["ticketWildCard"].ToString();
            int fromCountry = nvc["fromCountry"] == null ? 0 : Convert.ToInt32(nvc["fromCountry"]);
            int fromCity = nvc["fromCity"] == null ? 0 : Convert.ToInt32(nvc["fromCity"]);
            int toCountry = nvc["toCountry"] == null ? 0 : Convert.ToInt32(nvc["toCountry"]);
            int toCity = nvc["toCity"] == null ? 0 : Convert.ToInt32(nvc["toCity"]);
            int weightMin = nvc["weightMin"] == null ? 0 : Convert.ToInt32(nvc["weightMin"]);
            int weightMax = nvc["weightMax"] == null ? 30 : Convert.ToInt32(nvc["weightMax"]);
            //TODO- burada conditional linq nasıl yazılıyor bilmiyorum. if/else kullanmadan, olmayan query parametrelerini linq'e dahil etmeyelim
            //TODO- object mapping yapılmalı
            var results = db.Travels.Where(t => 
                   ((wildCard == "" ? true : (t.explanation.Contains(wildCard)
                   || t.fromCountry.name.Contains(wildCard)
                   || t.toCountry.name.Contains(wildCard)
                   || (t.toCityId.HasValue && t.toCity.name.ToString().Contains(wildCard) )
                   || (t.fromCityId.HasValue &&  t.fromCity.name.ToString().Contains(wildCard))))
                   && (fromCountry == 0 ? true : t.fromCountryId == fromCountry)
                   && (fromCity == 0 ? true : t.fromCityId == fromCity)
                   && (toCountry == 0 ?  true : t.toCountryId == toCountry)
                   && (toCity == 0 ? true : t.toCityId == toCity)
                   && (weightMin == 0 ? true : t.availableWeight > weightMin)
                   && (weightMax == 30 ? true : t.availableWeight < weightMax))
         ).ToList();
          var results2 = results
         .Select(t => new TravelDTO()
         {
             id = t.id,
             ownerId = t.ownerId,
             UserName = t.owner.UserName,
             firstName = t.owner.firstName,
             photo = t.owner.photo,
             fromCity = t.fromCity,
             fromCountry = t.fromCountry,
             toCity = t.toCity,
             toCountry = t.toCountry,
             availableWeight = t.availableWeight,
             availableVolume = t.availableVolume,
             startDate = t.startDate,
             finishDate = t.finishDate,
             itemType = t.itemType,
             price = t.price,
             explanation = t.explanation
         }).ToList();

            return results2;
        }

        // GET: api/Travels/5
        [ResponseType(typeof(TravelDTO))]
        public async Task<IHttpActionResult> GetTravel(int id)
        {
            Travel t = await db.Travels.FindAsync(id);
            if (t == null)
            {
                return NotFound();
            }

            TravelDTO travelDto = new TravelDTO() {
                id = t.id,
                ownerId = t.ownerId,
                UserName = t.owner.UserName,
                firstName = t.owner.firstName,
                photo = t.owner.photo,
                fromCity = t.fromCity,
                fromCountry = t.fromCountry,
                toCity = t.toCity,
                toCountry = t.toCountry,
                availableWeight = t.availableWeight,
                availableVolume = t.availableVolume,
                startDate = t.startDate,
                finishDate = t.finishDate,
                itemType = t.itemType,
                price = t.price,
                explanation = t.explanation
            };

            return Ok(travelDto);
        }

        // PUT: api/Travels/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutTravel(int id, Travel travel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != travel.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(travel).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TravelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Travels
        [ResponseType(typeof(NewTravelDto))]
        public async Task<IHttpActionResult> PostTravel(NewTravelDto t)
        {
            var usr = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            Travel travel = new Travel() {
                ownerId = usr.Id,
                fromCityId = t.fromCity,
                fromCountryId = t.fromCountry,
                toCityId = t.toCity,
                toCountryId = t.toCountry,
                availableWeight = t.availableWeight,
                availableVolume = t.availableVolume,
                startDate = t.startDate,
                finishDate = t.finishDate,
                itemType = t.itemType,
                price = t.price,
                explanation = t.explanation
            };

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try {
                db.Travels.Add(travel);
                await db.SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {

            }
            return CreatedAtRoute("DefaultApi", new { id = travel.id }, travel);
        }

        // DELETE: api/Travels/5
        //[ResponseType(typeof(Travel))]
        //public async Task<IHttpActionResult> DeleteTravel(int id)
        //{
        //    Travel travel = await db.Travels.FindAsync(id);
        //    if (travel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Travels.Remove(travel);
        //    await db.SaveChangesAsync();

        //    return Ok(travel);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TravelExists(int id)
        {
            return db.Travels.Count(e => e.id == id) > 0;
        }
    }
}