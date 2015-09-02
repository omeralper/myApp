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
        public IQueryable<Request> GetRequests()
        {
            return db.Requests;
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