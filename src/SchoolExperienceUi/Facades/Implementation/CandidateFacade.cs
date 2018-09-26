using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceBaseTypes;
using SchoolExperienceUiShared.Facades.Implementation;

namespace SchoolExperienceUi.Facades.Implementation
{
    internal class CandidateFacade : FacadeBase, ICandidateFacade
    {
        private const string BaseUrl = "v1/candidate/";

        public CandidateFacade(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
            : base(httpClientFactory, loggerFactory)
        {
        }

        public async Task<CreateDiaryEntryResult> CreateDiaryEntryAsync(string userId, DateTime start, DateTime end, DiaryEntryType type)
        {
            var request = new CreateDiaryEntryRequest
            {
                UserId = userId,
                Start = start,
                End = end,
                EntryType = type,
            };

            return await PostAsync<CreateDiaryEntryRequest, CreateDiaryEntryResult>($"{BaseUrl}creatediaryentry", request);
        }

        public async Task<DeleteDiaryEntryResult> DeleteDiaryEntryAsync(string userId, int id)
        {
            return await DeleteAsync<DeleteDiaryEntryResult>($"{BaseUrl}deletediaryentry?userId={userId}&id={id}");
        }

        public async Task<GetDiaryEntriesResponse> GetDiaryEntriesAsync(string userId, DateTime start, DateTime end)
        {
            return await GetStringAsync<GetDiaryEntriesResponse>($"{BaseUrl}getdiaryentries?userId={userId}&start={start:o}&end={end:o}");
        }
    }
}
