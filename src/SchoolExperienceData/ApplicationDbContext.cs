﻿using Microsoft.EntityFrameworkCore;

namespace SchoolExperienceData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entities.Candidate> Candidates { get; set; }
        public DbSet<Entities.CandidateDiary> CandidateDiarys { get; set; }
        public DbSet<Entities.School> Schools { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
