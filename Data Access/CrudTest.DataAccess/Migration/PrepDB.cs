using CrudTest.Application.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
//using Microsoft.AspNetCore.Builder;

namespace CrudTest.DataAccess.Migration
{
    public class PrepDB
    {
        public static void PrepPoupulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DBContext>());
            }
        }
        public static void SeedData(DBContext context)
        {
            Console.WriteLine("db migrating...");
            context.Database.Migrate();
            if (!context.Customers.Any())
            {
                Console.WriteLine("adding data...");
                context.Customers.AddRange(new Customers { FirstName = "Mojgan", LastName = "Kiyani", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "mkyani69@gmail.com",CreateDate=DateTime.Now}
               );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("already have data - not seeding");
            }
        }
    }
}
