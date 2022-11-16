using MicroservicesLearning.PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {}

        public DbSet<Platform> Platforms { get; set; }
    }
}
