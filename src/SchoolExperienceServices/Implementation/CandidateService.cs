using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceBaseTypes;
using SchoolExperienceData;
using SchoolExperienceData.Entities;

namespace SchoolExperienceServices.Implementation
{
    internal class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CandidateService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateDiaryEntryResult> CreateDiaryEntriesAsync(string userId, DateTime start, DateTime end, DiaryEntryType type)
        {
            if (end < start || end - start >= TimeSpan.FromDays(30))
            {
                throw new ArgumentOutOfRangeException(nameof(start), "Invalid start/end dates");
            }

            start = start.Date;
            end = end.Date;

            var candidate = _dbContext.Candidates.First(x => x.Id == Guid.Parse(userId));

            var existingEntries = await _dbContext.CandidateDiarys
                .Where(x => x.Candidate == candidate && x.When >= start && x.When <= end)
                .ToListAsync();

            var newEvents = new List<CandidateDiary>();

            var currentDay = start;
            while (currentDay != end)
            {
                // Make sure we don't overwrite any existing entries
                if (existingEntries.FirstOrDefault(x => x.When.Date == currentDay) == null)
                {
                    var entity = new CandidateDiary
                    {
                        Candidate = candidate,
                        EntryType = type,
                        When = currentDay,
                    };

                    newEvents.Add(entity);
                    _dbContext.CandidateDiarys.Add(entity);
                }

                currentDay = currentDay.AddDays(1);
            }

            await _dbContext.SaveChangesAsync();

            return new CreateDiaryEntryResult
            {
                Events = newEvents.Select(x=>new CreateDiaryEntryResult.EventItem
                {
                    Id = x.Id,
                    Result = CreateEventResult.Success,
                    When = x.When,
                }).ToList(),
            };
        }

        public async Task<DeleteDiaryEntryResult> DeleteDiaryEntriesAsync(string userId, int id)
        {
            var result = DeleteDiaryEntryResult.DeleteResult.None;
            var entry = _dbContext.CandidateDiarys.First(x => x.Candidate.Id == Guid.Parse(userId) && x.Id == id);

            switch (entry.EntryType)
            {
                case DiaryEntryType.Free:
                    result = DeleteDiaryEntryResult.DeleteResult.Deleted;
                    _dbContext.CandidateDiarys.Remove(entry);
                    await _dbContext.SaveChangesAsync();
                    break;

                case DiaryEntryType.Booked:
                    result = DeleteDiaryEntryResult.DeleteResult.NotAllowed;
                    break;
            }

            return new DeleteDiaryEntryResult
            {
                Result = result,
            };
        }

        public async Task<GetDiaryEntriesResponse> GetDiaryEventsAsync(string userId, DateTime start, DateTime end)
        {
            var candidateUserId = Guid.Parse(userId);
            var events = await _dbContext.CandidateDiarys
                .Where(x => x.Candidate.Id == candidateUserId && x.When >= start && x.When <= end)
                .Select(x => new GetDiaryEntriesResponse.DiaryEvent
                {
                    EntryType = x.EntryType,
                    Id = x.Id,
                    When = x.When,
                    Title = x.EntryType == DiaryEntryType.Booked ? x.School.Name : null,
                })
                .ToListAsync();

            return new GetDiaryEntriesResponse
            {
                Events = events,
            };
        }
    }
}
