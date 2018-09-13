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
        public SchoolFacade(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
            : base(httpClientFactory, loggerFactory)
        {
        }

        public async Task<FindSchoolsResult> FindSchoolsAsync(string postCode, Distance searchDistance)
        {
            return await GetStringAsync<FindSchoolsResult>($"/api/school/findschools?postcode={postCode}&distance={searchDistance.Metres:F0}");
        }

    }
}
