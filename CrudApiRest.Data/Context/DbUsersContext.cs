using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Data.Context
{
    public class DbUsersContext : DbContext, IDbUsersContext
    {
        public DbSet<User> Users { get; set; }

        public DbUsersContext(DbContextOptions<DbUsersContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .ForSqlServerIsMemoryOptimized();
        }
    }
}
