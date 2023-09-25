using Ankand.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace Ankand.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:ankand-app-server.database.windows.net,1433;Initial Catalog=ankand-db;Persist Security Info=False;User ID=ankand-admin;Password=Zxasqw1@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }
       
       
        public DbSet<Produkti> Poste { get; set; }
        public DbSet<Oferta> Oferta { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShopinCartItem> ShopinCartItem { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
    }
}
