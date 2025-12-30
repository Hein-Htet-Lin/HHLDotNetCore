using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HHLDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 44));
                optionsBuilder.UseMySql(connectionString,serverVersion);
                // Version ကို အလိုအလျောက် စစ်ဆေးခိုင်းခြင်း
                // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                
            }
        }

        public DbSet<BlogDataModels> Blogs {get;set;}
    }
}