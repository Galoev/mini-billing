using Microsoft.EntityFrameworkCore;

namespace Billing.WebApi.Repositories.Models
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

        public virtual DbSet<ComponentDbo> Components { get; set; }
        public virtual DbSet<CustomerDbo> Customers { get; set; }
        public virtual DbSet<GoodDbo> Goods { get; set; }
        public virtual DbSet<GoodComponentLinkDbo> GoodComponents { get; set; }
        public virtual DbSet<OrderDbo> Orders { get; set; }
        public virtual DbSet<OrderGoodLinkDbo> OrderGoods { get; set; }

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
            OnModelCreatingCustomer(modelBuilder);
            OnModelCreatingComponent(modelBuilder);
            OnModelCreatingGood(modelBuilder);
            OnModelCreatingOrder(modelBuilder);
            OnModelCreatingGoodComponent(modelBuilder);
            OnModelCreatingOrderGood(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void OnModelCreatingCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDbo>(entity =>
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
        }

        private void OnModelCreatingOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDbo>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeliveryStatus).HasColumnName("delivery_status");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("creation_date");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne<CustomerDbo>()
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_customer_id_fkey");
            });
        }

        private void OnModelCreatingGood(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodDbo>(entity =>
            {
                entity.ToTable("Good");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.QuantityType).HasColumnName("quantity_type");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.Property(e => e.Description).HasColumnName("description");
            });
        }

        private void OnModelCreatingComponent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComponentDbo>(entity =>
            {
                entity.ToTable("Component");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.QuantityType).HasColumnName("quantity_type");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.Property(e => e.Description).HasColumnName("description");        
            });
        }

        private void OnModelCreatingOrderGood(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderGoodLinkDbo>(entity => 
            { 
                entity.ToTable("OrderGood");

                entity.HasKey(e => new { e.OrderId, e.GoodId })
                    .HasName("OrderGood_pkey");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.GoodId).HasColumnName("good_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne<GoodDbo>()
                    .WithMany()
                    .HasForeignKey(d => d.GoodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderGood_good_id_fkey");

                entity.HasOne<OrderDbo>()
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderGood_order_id_fkey");
            });
        }

        private void OnModelCreatingGoodComponent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodComponentLinkDbo>(entity =>
            {
                entity.HasKey(e => new { e.GoodId, e.ComponentId })
                    .HasName("GoodComponent_pkey");

                entity.ToTable("GoodComponent");

                entity.Property(e => e.GoodId).HasColumnName("good_id");

                entity.Property(e => e.ComponentId).HasColumnName("component_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne<ComponentDbo>()
                    .WithMany()
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GoodComponent_component_id_fkey");

                entity.HasOne<GoodDbo>()
                    .WithMany()
                    .HasForeignKey(d => d.GoodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GoodComponents_goods_id_fkey");
            });
        }
    }
}
