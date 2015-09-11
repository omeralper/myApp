using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appServer.Models
{
    public enum MessageOwnerState { Active, Deleted }
    public enum MessageRecieverState { Unread, Read, Deleted }
    public enum ConversationState { Active, Deleted }
    public class Message
    {
        public int id { get; set; }

        [Required]
        [ForeignKey("owner")]
        public string ownerId { get; set; }
        public virtual ApplicationUser owner { get; set; }
        public MessageOwnerState ownerState { get; set; }
        public MessageRecieverState recieverState { get; set; }
        [Required]
        [StringLength(2000)]
        public string messageBody { get; set; }
        [Required]
        public DateTime messageDate { get; set; }
        [ForeignKey("conversation")]
        [Required]
        public int conversationId { get; set; }
        public Conversation conversation { get; set; }
        public int messageState { get; set; }
    }

    
    public class Conversation
    {
        public int id { get; set; }
        [Required]
        [ForeignKey("firstUser")]
        public string firstUserId { get; set; }
        public virtual ApplicationUser firstUser { get; set; }
        public ConversationState firstUserState { get; set; }

        [Required]
        [ForeignKey("secondUser")]
        public string secondUserId { get; set; }
        public virtual ApplicationUser secondUser { get; set; }
        public ConversationState secondUserState { get; set; }

        public virtual IList<Message> messages { get; set; }

        [ForeignKey("travel")]
        public int? travelId { get; set; }
        public virtual Travel travel { get; set; }

        [ForeignKey("request")]
        public int? requestId { get; set; }
        public virtual Request request { get; set; }

        public Conversation()
        {
            messages = new List<Message>();
        }
    }

    public class ConversationListItemDTO
    {
        public int id { get; set; }
        public MessageDTO lastMessage { get; set; }
    }

    public class ConversationDTO
    {
        public int id { get; set; }
        public IEnumerable<MessageDTO> messages { get; set; }
        public UserDTO otherUser { get; set; }
        public TravelDTO travel { get; set; }
        public RequestDTO request { get; set; }
        public ConversationDTO()
        {
            messages = new List<MessageDTO>();
        }
    }

    public class NewConversationDTO
    {
        public string toUserId { get; set; }
        public NewMessageDTO message { get; set; }
        public int? TravelId { get; set; }
        public int? RequestId { get; set; }
    }

    public class NewMessageDTO
    {
        public int conversationId { get; set; }
        public string messageBody { get; set; }
        public string toUser { get; set; }
    }

    public class MessageDTO
    {
        public string messageBody { get; set; }
        public string ownerId { get; set; }
        public byte[] ownerPhoto { get; set; }
        public string ownerName { get; set; }
        public DateTime? date { get; set; }
        public MessageRecieverState receiverState { get; set; }
        public bool my { get; set; }
    }

    public class ConversationParameters
    {
        public int? id { get; set; }
        public string toUserId { get; set; }
        public int? travelId { get; set; }
        public int? requestId { get; set; }
    }
}
