using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appServer.Models
{

    public class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyAppDBContext")
        {
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<appServer.Models.Requestor> Requestors { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Travel> Travels { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Conversation> Conversations { get; set; }

        //public System.Data.Entity.DbSet<appServer.Models.ApplicationUser> ApplicationUser { get; set; }

    }
}
