using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project5EMDAStaffManagement.Models;

namespace Project5EMDAStaffManagement.Data
{
    public class StaffDbContext : DbContext
    {
        // Here we add in all the tables we are using.
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Reasons> Reasons { get; set; }
        public DbSet<SignOuts> SignOuts { get; set; }

        // We need 2 constructors, one is empty, and the other injects in DbContextOptions
        public StaffDbContext(DbContextOptions<StaffDbContext> options)
            : base(options)
        {
        }
        public StaffDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Project5EMDAStaffManagement.Models.FruitModel> FruitModel { get; set; }
    }
}
