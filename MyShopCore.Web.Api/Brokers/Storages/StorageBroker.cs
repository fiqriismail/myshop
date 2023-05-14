using Microsoft.EntityFrameworkCore;

namespace MyShopCore.Web.Api.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
