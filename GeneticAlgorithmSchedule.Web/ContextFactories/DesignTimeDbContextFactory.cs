using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneticAlgorithmSchedule.Web.ContextFactories
{
    public abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        protected string _databaseName;

        public T CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<T>();
            var connectionString = configuration.GetConnectionString(_databaseName);
            builder.UseSqlServer(connectionString);
            var dbContext = (T)Activator.CreateInstance(
                typeof(T),
                builder.Options);
            return dbContext;
        }
    }
}
