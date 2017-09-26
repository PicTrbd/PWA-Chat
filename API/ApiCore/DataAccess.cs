using ChatHexagone.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCore
{
    public class DataAccess : DbContext
    {
        public DataAccess() : base(new DbContextOptionsBuilder().UseSqlServer(GetBuiltConnectionString()).Options)
        { }

        private static string GetBuiltConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                ["Encrypt"] = "True",
                ["Trusted_Connection"] = "False",
                ["User ID"] = DatabaseSecrets.UserId,
                ["Server"] = DatabaseSecrets.ServerURL,
                ["Password"] = DatabaseSecrets.Password,
                ["Database"] = DatabaseSecrets.DatabaseName
            };
            return builder.ToString();
        }

        public DbSet<Channel> Chanels { get; set; }
        public DbSet<PushSubscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataMapping.ConfigureMappings(modelBuilder);
        }
    }
}
