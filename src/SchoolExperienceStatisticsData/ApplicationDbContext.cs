using Microsoft.EntityFrameworkCore;

namespace SchoolExperienceStatisticsData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entities.Counter> Counters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
