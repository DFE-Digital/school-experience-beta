using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceServices;

namespace SchoolExperienceApi.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        [Route("CreateDiaryEntry")]
        public async Task<IActionResult> CreateDiaryEntry([FromBody] CreateDiaryEntryRequest request)
        {
            var result = await _candidateService.CreateDiaryEntriesAsync(request.UserId, request.Start, request.End, request.EntryType);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDiaryEntry")]
        public async Task<IActionResult> DeleteDiaryEntry([FromQuery] string userId, int id)
        {
            var result = await _candidateService.DeleteDiaryEntriesAsync(userId, id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDiaryEntries")]
        public async Task<IActionResult> GetDiaryEntries([FromQuery] string userId, DateTime start, DateTime end)
        {
            var result = await _candidateService.GetDiaryEventsAsync(userId, start, end);

            return Ok(result);
        }
    }
}
