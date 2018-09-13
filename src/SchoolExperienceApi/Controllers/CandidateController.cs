using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;
using SchoolExperienceServices;

namespace SchoolExperienceApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        [Route("GetDiaryEntries")]
        public async Task<IActionResult> GetDiaryEntries(Guid userId, DateTime start, DateTime end)
        {
            var result = await _candidateService.GetDiaryEventsAsync(userId, start, end);

            return Ok(result);
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
        public async Task<IActionResult> DeleteDiaryEntry(Guid userId, int id)
        {
            var result = await _candidateService.DeleteDiaryEntriesAsync(userId, id);

            return Ok(result);
        }
    }
}