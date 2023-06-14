using Microsoft.EntityFrameworkCore;
using ShopOganicAPI.Models;
using System.Reflection;

namespace ShopOganicAPI.Context
{
    public class OganicDBContext : DbContext
    {
        private readonly string connectionString = @"Data Source=DESKTOP-ABEN704\SQLEXPRESS;Initial Catalog=ShopOganic;User ID=sa;Password=1;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        
        protected OganicDBContext(DbContextOptions options) : base(options)
        {
        }

        public OganicDBContext()
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDetail> CategoryDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PaymentMenthod> PaymentMenthods { get; set; }
        public DbSet<PaymentMenthodDetail> PaymentMenthodDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShipAddress> ShipAddresses { get; set; }
        public DbSet<ShipMenthod> ShipMenthods { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
