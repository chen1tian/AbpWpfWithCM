using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AbpWpfWithCMDbContext>
    {
        public AbpWpfWithCMDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<AbpWpfWithCMDbContext>();
            optionsBuilder.UseSqlite(connString);
            return new AbpWpfWithCMDbContext(optionsBuilder.Options);
        }
    }
}
