﻿using CourseSalesAPI.Domain.Entities;
using CourseSalesAPI.Domain.Entities.Common;
using CourseSalesAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
      
      
       

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
      
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchasedCourse> PurchasedCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
          /*  builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);*/

            builder.Entity<Order>()
              .HasOne(o => o.User)
              .WithMany(u => u.Orders)
              .HasForeignKey(o => o.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
       .HasMany(o => o.OrderItems)
       .WithOne(oi => oi.Order)
       .HasForeignKey(oi => oi.OrderId)
       .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(builder);

           
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           

            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }



    }
}
