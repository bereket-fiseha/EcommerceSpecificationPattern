using Domain.Entity.Order;
using Domain.Entity.Registration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Common
{
    public class ECDbContext : DbContext
    {


        public ECDbContext(DbContextOptions options) : base(options) { }

     public   DbSet<Item> Items { get; set; }
     public   DbSet<ItemCategory> ItemCategories { get; set; }
       public DbSet<Customer> Customers { get; set; }
      public  DbSet<OrderCart> OrderCarts { get; set; }
      public  DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECDbContext).Assembly);

    }
}
