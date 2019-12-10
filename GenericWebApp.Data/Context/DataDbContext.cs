using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using GenericWebApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace GenericWebApp.Data.Context
{
    public class DataDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DataDbContext()
            : base()
        {
        }

        public DataDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Player>();
        }


    }
}
