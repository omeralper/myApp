using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appServer.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=MyAppDBContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Travel>()
                 .HasOptional(t => t.toCity)
                 .WithMany()
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Travel>()
                 .HasOptional(t => t.fromCity)
                 .WithMany()
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Travel>()
                 .HasRequired(t => t.fromCountry)
                 .WithMany()
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Travel>()
                 .HasRequired(t => t.toCountry)
                 .WithMany()
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Request>()
                 .HasRequired(t => t.toCity)
                 .WithMany()
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                 .HasOptional(t => t.fromCity)
                 .WithMany()
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                 .HasRequired(t => t.fromCountry)
                 .WithMany()
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Request>()
                 .HasRequired(t => t.toCountry)
                 .WithMany()
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Message>()
               .HasRequired(t => t.owner)
               .WithMany()
               .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //   .HasRequired(t => t.toUser)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
              .HasRequired(t => t.firstUser)
              .WithMany()
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
               .HasRequired(t => t.secondUser)
               .WithMany()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
               .HasOptional(t => t.travel)
               .WithMany()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
               .HasOptional(t => t.request)
               .WithMany()
               .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<appServer.Models.Request> Requests { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Travel> Travels { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Conversation> Conversations { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<appServer.Models.Currency> Currencies { get; set; }

        //public System.Data.Entity.DbSet<appServer.Models.ApplicationUser> ApplicationUser { get; set; }

    }
}
