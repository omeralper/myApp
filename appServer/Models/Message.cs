using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appServer.Models
{
    //omer:Bu mesajlaşma kısmını olabildiğince generic yapalım. Başka programlarda da kullanırız.
    public class Message
    {
        public int id { get; set; }
        //omer:from, to diye (reserved word) isimlendirmek istemedim, sql tarafında hataya sebep oluyo.
        public string fromMessage { get; set; }
        public string toMessage { get; set; }
        [StringLength(2000)]
        public string body { get; set; }
        public int readStatus { get; set; }
        public DateTime messageDate { get; set; }

        //omer:rastgele chatleşme olmayacağı için... ilani verilmiş olan birşey hakkında konuşulcak. O ilanın id'si
        [NotMapped]
        public int ItemId { get; set; }
    }

    
    public class Conversation
    {
        public int id { get; set; }
        public string fromMessage { get; set; }
        public string toMessage { get; set; }
        public virtual IList<Message> messages { get; set; }
        //omer:rastgele chatleşme olmayacağı için... ilani verilmiş olan birşey hakkında konuşulcak. O ilanın id'si
        [NotMapped]
        public int ItemId { get; set; }
        public Conversation()
        {
            messages = new List<Message>();
        }
    }
}
