using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project_1_GSG.Models
{
    public partial class restaurantdbContext : DbContext
    {
        private bool IgnoreFilter;
        public restaurantdbContext()
        {
        }

        public restaurantdbContext(DbContextOptions<restaurantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Restaurantmenu> Restaurantmenus { get; set; }
        public virtual DbSet<CsvView> CsvViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;port=3307;user=root;password=23_6_2000Taher;database=restaurantdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<CsvView>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.ResturantName).HasColumnType("varchar(255)");

                entity.Property(e => e.NumberOfOrderCustomer).HasColumnType("int");

                entity.Property(e => e.ProfitInUsd).HasColumnType("int");

                entity.Property(e => e.ProfitInNis).HasColumnType("int");

                entity.Property(e => e.TheBestSellingMeal).HasColumnType("varchar(255)");

                entity.Property(e => e.MostPurchasedCustomer).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => new { e.ResturantMenuId, e.CustomerId })
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.CustomerId, "Order_Customer_idx");

                entity.Property(e => e.ResturantMenuId).HasColumnType("int unsigned");

                entity.Property(e => e.CustomerId).HasColumnType("int unsigned");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_Customer");

                entity.HasOne(d => d.ResturantMenu)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ResturantMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_ResurantMenue");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Restaurantmenu>(entity =>
            {
                entity.ToTable("restaurantmenu");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RestaurantId, "restaurantId_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PriceInNis).HasDefaultValueSql("'10'");

                entity.Property(e => e.PriceInUsd).HasDefaultValueSql("'10'");

                entity.Property(e => e.Quantity).HasDefaultValueSql("'10'");

                entity.Property(e => e.RestaurantId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("restaurantId");

                entity.HasOne(d => d.Restaurant)
                    .WithOne(p => p.Restaurantmenu)
                    .HasForeignKey<Restaurantmenu>(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resturantmenue_resturant");
            });

            modelBuilder.Entity<Customer>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Order>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Restaurantmenu>().HasQueryFilter(a => !a.Archived || IgnoreFilter);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
