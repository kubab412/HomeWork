using Microsoft.EntityFrameworkCore;
using SportMarket.Domain;

namespace SportMarket.DataBase
{
    public class SportMarketDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public SportMarketDbContext(DbContextOptions<SportMarketDbContext> options) : base(options)
        {

        }
    }
}
