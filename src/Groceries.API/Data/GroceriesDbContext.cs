using Groceries.API.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Groceries.API.Data
{
    public class GroceriesDbContext : DbContext
    {
        public GroceriesDbContext(DbContextOptions<GroceriesDbContext> options)
            : base(options)
        {
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grocery>();
            
            base.OnModelCreating(modelBuilder); 
        }
         
        
        public DbSet<Grocery> Groceries { get; set; }
    }
     
}
