namespace WebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LocationDbContext : DbContext
    {
        public LocationDbContext()
            : base("name=LocationDbCon")
        {
        }

        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.State)
                .IsUnicode(false);
        }
    }
}
