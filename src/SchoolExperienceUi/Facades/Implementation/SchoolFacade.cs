using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Facades.Implementation
{
    internal class SchoolFacade : FacadeBase, ISchoolFacade
    {
        private const string BaseUrl = "/api/school/";

        public SchoolFacade(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
            : base(httpClientFactory, loggerFactory)
        {
        }

        public Task<BookCandidateResponse> BookCandidate(string userId, string schoolId, string candidateId, DateTime when) 
            => throw new NotImplementedException();

        public Task<CancelBookingResponse> CancelBooking(string userId, int id) 
            => throw new NotImplementedException();

        public async Task<FindSchoolsResponse> FindSchoolsAsync(string postCode, Distance searchDistance)
        {
            return await GetStringAsync<FindSchoolsResponse>($"/api/school/findschools?postcode={postCode}&distance={searchDistance.Metres:F0}");
        }

        public async Task<GetDiaryEntriesResponse> GetDiaryEntriesAsync(string userId, string schoolId, DateTime start, DateTime end)
        {
            return await GetStringAsync<GetDiaryEntriesResponse>($"{BaseUrl}getdiaryentries?userId={userId}&schoolId={schoolId}&start={start:o}&end={end:o}");
        }
    }
}
