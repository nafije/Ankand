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
            optionsBuilder.UseSqlServer("Data Source=nafije-pc\\sqlexpress;Initial Catalog=Ankand-db;Integrated Security=True;Pooling=False; trustServerCertificate = true");

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
