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
using appServer.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Collections.Specialized;
using System.Web;

namespace appServer.Controllers
{
    public class RequestsController : ApiController
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

        // GET: api/Requests
        public IList<RequestDTO> GetRequests()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string wildCard = nvc["ticketWildCard"] == null ? "" : nvc["ticketWildCard"].ToString();
            int fromCountry = nvc["fromCountry"] == null ? 0 : Convert.ToInt32(nvc["fromCountry"]);
            int fromCity = nvc["fromCity"] == null ? 0 : Convert.ToInt32(nvc["fromCity"]);
            int toCountry = nvc["toCountry"] == null ? 0 : Convert.ToInt32(nvc["toCountry"]);
            int toCity = nvc["toCity"] == null ? 0 : Convert.ToInt32(nvc["toCity"]);
            int weightMin = nvc["weightMin"] == null ? 0 : Convert.ToInt32(nvc["weightMin"]);
            int weightMax = nvc["weightMax"] == null ? 30 : Convert.ToInt32(nvc["weightMax"]);
            //int volumeMin = nvc["volumeMin"] == null ? 0 : Convert.ToInt32(nvc["volumeMin"]);
            //int volumeMax = nvc["volumeMax"] == null ? 30 : Convert.ToInt32(nvc["volumeMax"]);
            //TODO- burada conditional linq nasıl yazılıyor bilmiyorum. if/else kullanmadan, olmayan query parametrelerini linq'e dahil etmeyelim
            //TODO- object mapping yapılmalı
            var results = db.Requests.Where(t =>
                   ((wildCard == "" ? true : (t.explanation.Contains(wildCard)
                   || t.fromCountry.name.Contains(wildCard)
                   || t.toCountry.name.Contains(wildCard)
                   || (t.toCityId.HasValue && t.toCity.name.ToString().Contains(wildCard))
                   || (t.fromCityId.HasValue && t.fromCity.name.ToString().Contains(wildCard))))
                   && (fromCountry == 0 ? true : t.fromCountryId == fromCountry)
                   && (fromCity == 0 ? true : t.fromCityId == fromCity)
                   && (toCountry == 0 ? true : t.toCountryId == toCountry)
                   && (toCity == 0 ? true : t.toCityId == toCity)
                   && (weightMin == 0 ? true : t.estimatedWeight > weightMin)
                   && (weightMax == 30 ? true : t.estimatedWeight < weightMax))
         ).ToList();
            var results2 = results
           .Select(t => new RequestDTO()
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
               estimatedWeight = t.estimatedWeight,
               price = t.price,
               itemType = t.itemType,
               explanation = t.explanation
           }).ToList();

            return results2;
        }

        // GET: api/Requests/5
        [ResponseType(typeof(RequestDTO))]
        public async Task<IHttpActionResult> GetRequest(int id)
        {
            Request r = await db.Requests.FindAsync(id);
            if (r == null)
            {
                return NotFound();
            }

            RequestDTO requestDTO = new RequestDTO() {
                id = r.id,
                ownerId = r.ownerId,
                UserName = r.owner.UserName,
                firstName = r.owner.firstName,
                photo = r.owner.photo,
                fromCity = r.fromCity,
                fromCountry = r.fromCountry,
                toCity = r.toCity,
                toCountry = r.toCountry,
                title= r.title,
                price = r.price,
                estimatedWeight = r.estimatedWeight,
                explanation = r.explanation
            };


            return Ok(requestDTO);
        }

        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.id)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> PostRequest(NewRequestDTO r)
        {
            var usr = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            Request request = new Request() {
                ownerId = usr.Id,
                fromCityId = r.fromCity,
                fromCountryId = r.fromCountry,
                toCityId = r.toCity,
                toCountryId = r.toCountry,
                estimatedWeight = r.estimatedWeight,
                estimatedVolume = r.estimatedVolume,
                itemType = r.itemType,
                title  = r.title,
                price = r.price,
                currencyId = r.currency,
                explanation = r.explanation
            };
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Requests.Add(request);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = request.id }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> DeleteRequest(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            await db.SaveChangesAsync();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.id == id) > 0;
        }
    }
}