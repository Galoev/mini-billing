using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Billing.WebApi.DAL.Models
{
    public partial class BillingContext : DbContext
    {
        public BillingContext()
        {
        }

        public BillingContext(DbContextOptions<BillingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComponentEntity> Components { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<GoodsEntity> Goods { get; set; }
        public virtual DbSet<GoodsComponent> GoodsComponents { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderGoods> OrderGoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=billing-db;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComponentEntity>(entity =>
            {
                entity.ToTable("Component");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.QuantityUnit).HasColumnName("quantity_unit");
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Phone, "Customer_phone_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.AdditionalInfo).HasColumnName("additional_info");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<GoodsEntity>(entity =>
            {
                entity.ToTable("Goods");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.QuantityUnit).HasColumnName("quantity_unit");
            });

            modelBuilder.Entity<GoodsComponent>(entity =>
            {
                entity.HasKey(e => new { e.GoodsId, e.ComponentId })
                    .HasName("GoodsComponent_pkey");

                entity.ToTable("GoodsComponent");

                entity.Property(e => e.GoodsId).HasColumnName("goods_id");

                entity.Property(e => e.ComponentId).HasColumnName("component_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.GoodsComponents)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GoodsComponent_component_id_fkey");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.GoodsComponents)
                    .HasForeignKey(d => d.GoodsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GoodsComponent_goods_id_fkey");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeliverStatus).HasColumnName("deliver_status");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("order_date");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_customer_id_fkey");
            });

            modelBuilder.Entity<OrderGoods>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.GoodsId })
                    .HasName("OrderGoods_pkey");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.GoodsId).HasColumnName("goods_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.OrderGoods)
                    .HasForeignKey(d => d.GoodsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderGoods_goods_id_fkey");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderGoods)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderGoods_order_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
