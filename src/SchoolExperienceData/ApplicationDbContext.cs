using Microsoft.EntityFrameworkCore;

namespace SchoolExperienceData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entities.Booking> Bookings{ get; set; }
        public DbSet<Entities.Candidate> Candidates { get; set; }
        public DbSet<Entities.CandidateCalendar> CandidateCalendar { get; set; }
        public DbSet<Entities.GitisData> GitisUsers { get; set; }
        public DbSet<Entities.Notification> Notifications { get; set; }
        public DbSet<Entities.School> Schools { get; set; }
        public DbSet<Entities.SchoolCalendar> SchoolCalendar { get; set; }
        public DbSet<Entities.SchoolUser> SchoolUsers { get; set; }
        public DbSet<Entities.Subject> Subjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Entities.SchoolSchoolUserJoinConfiguration());
        }
    }
}
