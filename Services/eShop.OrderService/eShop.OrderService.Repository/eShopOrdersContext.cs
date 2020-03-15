using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eShop.OrderService.Repository
{
    public partial class eShopOrdersContext : DbContext
    {
        public eShopOrdersContext()
        {
        }

        public eShopOrdersContext(DbContextOptions<eShopOrdersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ServiceTypeEnumrations> ServiceTypeEnumrations { get; set; }
        public virtual DbSet<StatusEnumrations> StatusEnumrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=eShop.Orders;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_ServiceType_Enumrations");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Status_Enumrations");
            });

            modelBuilder.Entity<ServiceTypeEnumrations>(entity =>
            {
                entity.ToTable("ServiceType_Enumrations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StatusEnumrations>(entity =>
            {
                entity.ToTable("Status_Enumrations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
