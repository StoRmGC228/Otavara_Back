using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class OtavaraDbContext:DbContext
    {
        public OtavaraDbContext(DbContextOptions<OtavaraDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RequestedCard> Cards { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }

        public virtual DbSet<BookedGood> BookedGoods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Participant>()
                .HasKey(ue => new { ue.UserId, ue.EventId });

            modelBuilder.Entity<Participant>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.SubscribedEvents)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<Participant>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ue => ue.EventId);

            modelBuilder.Entity<RequestedCard>()
                .HasOne(c => c.Requester)
                .WithMany(u => u.WishedCards)
                .HasForeignKey(c => c.RequesterId);

            modelBuilder.Entity<RequestedCard>()
                .HasOne(c => c.RequestedEvent)
                .WithMany(ev => ev.RequestedCards)
                .HasForeignKey(c => c.EventId);

            modelBuilder.Entity<BookedGood>()
                .HasKey(ug => new { ug.GoodId, ug.UserId });

            modelBuilder.Entity<BookedGood>()
                .HasOne(bg => bg.User)
                .WithMany(u => u.BookedGoods)
                .HasForeignKey(bg => bg.UserId);

            modelBuilder.Entity<BookedGood>()
                .HasOne(bg => bg.Good)
                .WithMany(g => g.Bookers)
                .HasForeignKey(g => g.GoodId);





        }
    }
}
