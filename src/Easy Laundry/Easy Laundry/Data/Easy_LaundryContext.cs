using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Easy_Laundry.Models;

namespace Easy_Laundry.Data
{
    public class Easy_LaundryContext : DbContext
    {
        public Easy_LaundryContext (DbContextOptions<Easy_LaundryContext> options)
            : base(options)
        {
        }

     public DbSet<Easy_Laundry.Models.OrderModel> OrderModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()"); 
        }

    }
}
