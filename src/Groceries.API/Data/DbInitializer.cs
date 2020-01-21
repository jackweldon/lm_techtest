using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Groceries.API.Data;
using Groceries.API.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Groceries.API
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public static class DbContextOptionsExtensions
    {
        public static void InitializeDb(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitialize.Initialize();
            }
        }
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void Initialize()
        {
            string filePath = "Data\\Technical Task - Grocery Prices - CSharp.csv";
            IEnumerable<Grocery> result;
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<GroceriesDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (context.Groceries.Any())
                    {
                        return;
                    }

                    var environment = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                    var path = Path.Combine(environment.ContentRootPath, filePath);
                    var csvParser = serviceScope.ServiceProvider.GetRequiredService<ICSVParser<Grocery>>();
                    var items = csvParser.Deserialize(path);
                   
                    result = items.ToList();

                    context.Groceries.AddRange(result);
                    context.SaveChanges();
                }
            } 
        }
    }
}
