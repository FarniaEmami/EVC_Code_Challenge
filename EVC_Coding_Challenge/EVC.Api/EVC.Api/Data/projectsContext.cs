using EVC.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVC.Api.Data
{
    public class projectsContext : DbContext
    {
        public projectsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserInvestments> UserInvestments { get; set; }


    }
}
