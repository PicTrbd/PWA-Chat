using System.Data.Entity;
using Api_v2.Models;

namespace Api_v2
{
    public static class DataMapping
    {
        public static void ConfigureMappings(DbModelBuilder modelBuilder)
        {
            MapEntityDbEntry(modelBuilder);
        }

        private static void MapEntityDbEntry(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomModel>().ToTable("Rooms");
            modelBuilder.Entity<MessageModel>().ToTable("Messages");
            modelBuilder.Entity<PushSubscriptionModel>().ToTable("PushSubscriptions");
        }
    }
}
