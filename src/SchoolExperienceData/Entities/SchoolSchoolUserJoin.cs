using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolExperienceData.Entities
{
    public class SchoolSchoolUserJoin
    {
        public School School { get; set; }
        public int SchoolId { get; set; }
        public SchoolUser SchoolUser { get; set; }
        public int SchoolUserId { get; set; }
    }

    internal class SchoolSchoolUserJoinConfiguration : IEntityTypeConfiguration<SchoolSchoolUserJoin>
    {
        public void Configure(EntityTypeBuilder<SchoolSchoolUserJoin> builder)
        {
            builder.HasKey(bc => new { bc.SchoolId, bc.SchoolUserId});

            builder
                .HasOne(bc => bc.School)
                .WithMany(b => b.AssociatedUsers)
                .HasForeignKey(bc => bc.SchoolId);

            builder
                .HasOne(bc => bc.SchoolUser)
                .WithMany(c => c.Associations)
                .HasForeignKey(bc => bc.SchoolUserId);
        }
    }
}
