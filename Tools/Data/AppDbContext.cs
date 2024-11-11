using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiagnoseMe.Models;

namespace DiagnoseMe.Tools.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Podaj connection string do bazy danych Azure SQL
            optionsBuilder.UseSqlServer("REDACTED_CONNECTION_STRING");
        }
    }
}
