using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;
using SchoolExperienceData;

namespace SchoolExperienceServices.Implementation
{
    internal class SchoolService : ISchoolService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SchoolService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<BookCandidateResponse> CreateBooking(string userId, string schoolId, string candidateId, DateTime when)
            => throw new NotImplementedException();

        public async Task<FindSchoolsResponse> FindSchoolsAsync(string postCode, Distance searchDistance)
        {
            var schools = new List<FindSchoolsResponse.School>();

            if (searchDistance.Kilometres >= 1)
            {
                schools.Add(new FindSchoolsResponse.School
                {
                    SchoolId = "1",
                    SchoolType = SchoolType.Primary,
                    Name = "St Andrew's Church of England Primary School & Nursery",
                    ContactName = "Mrs. J A Rainford",
                    Address = "Union Road, OL12 9QA",
                    Distance = new Distance { Metres = 1500 },
                });
            }

            if (searchDistance.Kilometres >= 2)
            {
                schools.Add(new FindSchoolsResponse.School
                {
                    SchoolId = "2",
                    SchoolType = SchoolType.Primary,
                    Name = "Smithy Bridge Primary",
                    ContactName = "Mrs J George",
                    Address = "Bridgenorth Drive, Littleborough. OL15 0DY",
                    Distance = new Distance { Metres = 2150 },
                });
            }

            if (searchDistance.Kilometres >= 3)
            {
                schools.Add(new FindSchoolsResponse.School
                {
                    SchoolId = "3",
                    SchoolType = SchoolType.Secondary,
                    Name = "Wardle Academy",
                    ContactName = "Mrs. Zoe Long",
                    Address = "Birch Rd, Rochdale OL12 9RD",
                    Distance = new Distance { Metres = 3500 },
                });
            }

            return await Task.FromResult(new FindSchoolsResponse
            {
                SearchDistance = searchDistance,
                Schools = schools,
            });
        }

        public async Task<GetDiaryEntriesResponse> GetDiaryEventsAsync(string userId, string schoolId, DateTime start, DateTime end)
        {
            var entries = await _dbContext.SchoolDiary
                .Where(x => x.School.Id == schoolId && x.When >= start && x.When <= end)
                .Select(x => new GetDiaryEntriesResponse.DiaryEvent
                {
                    Id = x.Id,
                    When = x.When,
                    CandidateName = x.CandidateDiary.Candidate.Name,
                    CandidateSubject = x.CandidateDiary.Candidate.Subject,
                    EntryType = DiaryEntryType.Booked,
                })
                .ToListAsync();

            return new GetDiaryEntriesResponse { Events = entries };
        }
    }
}
