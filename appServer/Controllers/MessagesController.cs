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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using appServer.Hubs;

namespace appServer.Controllers
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Messages
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.id)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(NewMessageDTO))]
        public async Task<IHttpActionResult> PostMessage(NewMessageDTO m)
        {
            Message message = new Message() {
                messageBody = m.messageBody,
                conversationId = m.conversationId,
                messageDate = DateTime.Now,
                ownerId = User.Identity.GetUserId(),
                recieverState = MessageRecieverState.Unread,
                ownerState = MessageOwnerState.Active
            };
            
            db.Messages.Add(message);
            await db.SaveChangesAsync();

            message = db.Messages
                .Include(t => t.conversation)
                .FirstOrDefault(t => t.id == message.id);
                
            
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.User(m.toUser).sendMessage(m.messageBody);
            //context.Clients.All.sendMessage(m.messageBody);

            return CreatedAtRoute("DefaultApi", new { id = message.id }, m);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.id == id) > 0;
        }
    }
}