using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eShop.MobileRechargeService.Repository
{
    public partial class eShopMobileRechargesContext : DbContext
    {
        public eShopMobileRechargesContext()
        {
        }

        public eShopMobileRechargesContext(DbContextOptions<eShopMobileRechargesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Providers> Providers { get; set; }
        public virtual DbSet<RechargeOrders> RechargeOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=eShop.MobileRecharges;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Providers>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
