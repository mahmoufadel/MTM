
using System;
using Microsoft.EntityFrameworkCore;
using MTM.Domain;
using MTM.Domain.Model;

namespace MTM.Infra
{


    public class EFCoreDemoContext : DbContext
    {
        public EFCoreDemoContext(DbContextOptions<EFCoreDemoContext> options) : base(options)
        {
        }

        protected EFCoreDemoContext()
        {
        }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryTrans> InventoryTrans { get; set; }
        public DbSet<Asset> Assets { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasData(new Inventory()
            {
                Id = Guid.NewGuid(),
                Description = "OG description of the URL",
                Summary = "OG Title of the URL",
                
            });

           // modelBuilder.Entity<InventoryTrans>().HasOne<Inventory>();
            
        }
    }
}
