namespace CarDealer.Data
{
    using System.Data.SqlClient;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<PartCars>()
                .HasKey(pc => new { pc.CarId, pc.PartId });

            builder
                .Entity<Car>()
                .HasMany(c => c.Parts)
                .WithOne(pc => pc.Car)
                .HasForeignKey(c => c.CarId);

            builder
                .Entity<Part>()
                .HasMany(p => p.Cars)
                .WithOne(pc => pc.Part)
                .HasForeignKey(p => p.PartId);

            builder
                .Entity<Part>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p => p.SupplierId);

            builder
                .Entity<Sale>()
                .HasOne(s => s.Car)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CarId);

            builder
                .Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder
                .Entity<User>()
                .HasMany(u => u.Logs)
                .WithOne(l => l.User)
                .HasForeignKey(u => u.UserId);

            base.OnModelCreating(builder);
        }
    }
}
