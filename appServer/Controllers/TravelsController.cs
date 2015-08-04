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

namespace appServer.Controllers
{
    public class TravelsController : ApiController
    {
        private MyDBContext db = new MyDBContext();

        // GET: api/Travels
        /// <summary>
        /// Bu methoda parametre geliyor aslında. Query string'ten parametre geliyor. Eğer query çok uzun olursa (max 2000 char) bu methodu get değil post yapmamız gerek. 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Travel> GetTravels()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string wildCard = nvc["filterTravel"] == null ? "" : nvc["filterTravel"].ToString();
            //TODO- burada conditional linq nasıl yazılıyor bilmiyorum. if/else kullanmadan, olmayan query parametrelerini linq'e dahil etmeyelim
            IQueryable<Travel> results = db.Travels.Where( t => t.explanation.Contains(wildCard));

            return results;
        }

        // GET: api/Travels/5
        [ResponseType(typeof(Travel))]
        public async Task<IHttpActionResult> GetTravel(int id)
        {
            Travel travel = await db.Travels.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }

            return Ok(travel);
        }

        // PUT: api/Travels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTravel(int id, Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != travel.id)
            {
                return BadRequest();
            }

            db.Entry(travel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelExists(id))
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

        // POST: api/Travels
        [ResponseType(typeof(Travel))]
        public async Task<IHttpActionResult> PostTravel(Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Travels.Add(travel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = travel.id }, travel);
        }

        // DELETE: api/Travels/5
        [ResponseType(typeof(Travel))]
        public async Task<IHttpActionResult> DeleteTravel(int id)
        {
            Travel travel = await db.Travels.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }

            db.Travels.Remove(travel);
            await db.SaveChangesAsync();

            return Ok(travel);
        }

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