﻿using Ankand.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ankand.Data
{
    public class AppDbContext:DbContext
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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Poste>()
        //        .HasMany(p => p.Coments)
        //        .WithOne(c => c.Poste)
        //        .HasForeignKey(c => c.PostID);
        //}

        public DbSet<Produkti> Poste { get; set; }
        public DbSet<Oferta> Oferta { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShopinCartItem> ShopinCartItem { get; set; }
    }
}