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
    [Authorize]
    public class ConversationsController : ApiController
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

        // GET: api/Conversations
        public IQueryable<ConversationListItemDTO> GetConversations()
        {
            string userId = User.Identity.GetUserId();
            return db.Conversations
                .Where(t => (t.firstUserId == userId || t.secondUserId == userId) && t.messages.Count > 0)
                .Select(t => new ConversationListItemDTO() { id = t.id,
                    lastMessage = new MessageDTO() {
                    date = t.messages.OrderByDescending(m => m.messageDate).FirstOrDefault().messageDate ,
                    messageBody = t.messages.OrderByDescending(m => m.messageDate).FirstOrDefault().messageBody,
                    ownerName = t.messages.OrderByDescending(m => m.messageDate).FirstOrDefault().owner.firstName,
                    ownerPhoto = t.messages.OrderByDescending(m => m.messageDate).FirstOrDefault().owner.photo,
                    receiverState = t.messages.OrderByDescending(m => m.messageDate).FirstOrDefault().recieverState 
                }});
        }

        // GET: api/Conversations/5
        [ResponseType(typeof(ConversationDTO))]
        public async Task<IHttpActionResult> GetConversation(int? id, string toUserId, int? travelId, int? requestId)
        {
            var userId = User.Identity.GetUserId();
            Conversation conversation = null;
            if (id.HasValue)
            {
                conversation = await db.Conversations.FindAsync(id);
            }
            else
            {
                conversation = await db.Conversations.Where(
                    t => 
                    t.secondUserId == toUserId &&
                    t.firstUserId == userId &&
                ((requestId.HasValue && t.requestId == requestId) || (travelId.HasValue && t.travelId == travelId)))
                .FirstOrDefaultAsync();
            }
            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    firstUserId = User.Identity.GetUserId(),
                    secondUserId = toUserId,
                    travelId = travelId,
                    requestId = requestId
                };
                db.Conversations.Add(conversation);
                await db.SaveChangesAsync();

                conversation = db.Conversations
                    .Include(t => t.firstUser)
                    .Include(t => t.secondUser)
                    .Include(t => t.travel)
                    .Include(t => t.request)
                    .FirstOrDefault(t => t.id == conversation.id);
            }

            UserDTO otherUser = new UserDTO();
            if(conversation.firstUserId == userId)
            {
                otherUser.userId = conversation.secondUserId;
                otherUser.photo = conversation.secondUser.photo;
                otherUser.firstName = conversation.secondUser.firstName;
            }
            else
            {
                otherUser.userId = conversation.firstUserId;
                otherUser.photo = conversation.firstUser.photo;
                otherUser.firstName = conversation.firstUser.firstName;
            }


            TravelDTO travelDTO = null;
            if (conversation.travelId.HasValue)
            {
                travelDTO = new TravelDTO()
                {
                    id = conversation.travel.id,
                    explanation = conversation.travel.explanation,
                    availableWeight = conversation.travel.availableWeight,
                    price = conversation.travel.price,
                    fromCity = conversation.travel.fromCity,
                    toCity = conversation.travel.toCity,
                    fromCountry = conversation.travel.fromCountry,
                    toCountry = conversation.travel.toCountry,
                };
            }

            RequestDTO requestDTO = null;
            if (conversation.requestId.HasValue)
            {
                requestDTO = new RequestDTO()
                {
                    id = conversation.request.id,
                    explanation = conversation.request.explanation,
                    estimatedWeight = conversation.request.estimatedWeight,
                    price = conversation.request.price,
                    title = conversation.request.title,
                    fromCity = conversation.request.fromCity,
                    toCity = conversation.request.toCity,
                    fromCountry = conversation.request.fromCountry,
                    toCountry = conversation.request.toCountry,
                };
            }

            ConversationDTO conversationDTO = new ConversationDTO()
            {
                id = conversation.id,
                otherUser = otherUser,
                travel = travelDTO,
                request = requestDTO,
                messages = conversation.messages.Select(t => new MessageDTO() {
                    ownerId = t.ownerId,
                    my = t.ownerId == userId,
                    date = t.messageDate,
                    messageBody = t.messageBody,
                    ownerName = t.owner.firstName,
                    receiverState = t.recieverState
                })//.Take(10)
            };

            return Ok(conversationDTO);
        }

        // PUT: api/Conversations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConversation(int id, Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conversation.id)
            {
                return BadRequest();
            }

            db.Entry(conversation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
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

        // POST: api/Conversations
        [ResponseType(typeof(Conversation))]
        public async Task<IHttpActionResult> PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Conversations.Add(conversation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = conversation.id }, conversation);
        }

        // DELETE: api/Conversations/5
        [ResponseType(typeof(Conversation))]
        public async Task<IHttpActionResult> DeleteConversation(int id)
        {
            Conversation conversation = await db.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            db.Conversations.Remove(conversation);
            await db.SaveChangesAsync();

            return Ok(conversation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConversationExists(int id)
        {
            return db.Conversations.Count(e => e.id == id) > 0;
        }
    }
}