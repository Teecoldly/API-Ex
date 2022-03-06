using Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
namespace Commons.Extention
{
    public class ContextExtention : testdbContext
    {


        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            return builder.Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuation = GetConfiguration();
            optionsBuilder.UseMySQL(configuation.GetSection("ConnectionStrings").GetSection("db").Value);

        }
    }
}
