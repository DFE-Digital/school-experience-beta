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

        public async Task<BookCandidateResponse> BookCandidate(string userId, string schoolId, string candidateId, DateTime when)
        {
            var request = new BookCandidateRequest
            {
                UserId = userId,
                SchoolId = schoolId,
                CandidateId = candidateId,
                Date = when,
            };

            return await PostAsync<BookCandidateRequest, BookCandidateResponse>($"{BaseUrl}bookcandidate", request);
        }

        public async Task<CancelBookingResponse> CancelBooking(string userId, int id)
        {
            var request = new CancelBookingRequest
            {
                UserId = userId,
                BookingId = id,
            };

            return await PostAsync<CancelBookingRequest, CancelBookingResponse>($"{BaseUrl}cancelbooking", request);
        }

        public async Task<FindSchoolsResponse> FindSchoolsAsync(string postCode, Distance searchDistance)
        {
            return await GetStringAsync<FindSchoolsResponse>($"{BaseUrl}findschools?postcode={postCode}&distance={searchDistance.Metres:F0}");
        }

        public async Task<GetDiaryEntriesResponse> GetDiaryEntriesAsync(string userId, string schoolId, DateTime start, DateTime end)
        {
            return await GetStringAsync<GetDiaryEntriesResponse>($"{BaseUrl}getdiaryentries?userId={userId}&schoolId={schoolId}&start={start:o}&end={end:o}");
        }
    }
}
