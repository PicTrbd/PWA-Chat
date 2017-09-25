using System;
using System.Data.Entity;
using System.Data.SqlClient;
using Api_v2.Models;

namespace Api_v2
{
    public class DataAccess : DbContext
    {
        private volatile Type _dependency;

        public DataAccess() : base(GetBuiltConnectionString())
        {
            _dependency = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public virtual DbSet<RoomModel> Rooms { get; set; }
        public virtual DbSet<MessageModel> Messages { get; set; }
        public virtual DbSet<PushSubscriptionModel> Subscriptions { get; set; }

        public static string GetBuiltConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                ["Server"] = "fdv-sqlhost-dev.database.windows.net",
                ["User ID"] = "anthyme",
                ["Database"] = "fdv-sql-dev",
                ["Password"] = "FDVadmin123",
                ["Trusted_Connection"] = "False",
                ["Encrypt"] = "True"
            };
            return builder.ToString();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataAccess>(null);
            DataMapping.ConfigureMappings(modelBuilder);
        }
    }
}
