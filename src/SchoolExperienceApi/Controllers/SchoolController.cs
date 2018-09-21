using System;
using System.Collections.Generic;
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
    public  class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [Route("FindSchools")]
        public async Task<FindSchoolsResponse> FindSchools(string postCode, int distance)
        {
            var result = await _schoolService.FindSchoolsAsync(postCode, new Distance { Metres = distance });
            return result;
        }

        [HttpGet]
        [Route("GetDiaryEntries")]
        public async Task<IActionResult> GetDiaryEntries([FromQuery] GetDiaryEntriesRequest request)
        {
            var result = await _schoolService.GetDiaryEventsAsync(request.UserId, request.SchoolId, request.Start, request.End);

            return Ok(result);
        }


        [HttpGet]
        [Route("GetPlacementTotals")]
        public async Task<IActionResult> GetPlacementTotals()
        {
            var result = new GetPlacementTotalsResponse
            {
                TotalAdvertised = 12,
                TotalAvailable = 10,
                TotalBooked = 99,
                SubjectPlacements = new List<GetPlacementTotalsResponse.SubjectPlacement>
                {
                    new GetPlacementTotalsResponse.SubjectPlacement { Name = "English", Count = 10 },
                    new GetPlacementTotalsResponse.SubjectPlacement { Name = "French", Count = 25 },
                    new GetPlacementTotalsResponse.SubjectPlacement { Name = "Chemistry", Count = 37 },
                },
                SchoolPlacements = new List<GetPlacementTotalsResponse.SchoolPlacement>
                {
                    new GetPlacementTotalsResponse.SchoolPlacement { Name = "Hogwarts", Count = 25 },
                    new GetPlacementTotalsResponse.SchoolPlacement { Name = "St Trinian's", Count = 17 },
                    new GetPlacementTotalsResponse.SchoolPlacement { Name = "Grange Hill", Count = 42 },
                }
            };

            return Ok(result);
        }


        [HttpPost]
        [Route("BookCandidate")]
        public async Task<BookCandidateResponse> BookCandidate([FromBody] BookCandidateRequest request)
        {
            var result = await _schoolService.CreateBooking(request.UserId, request.SchoolId, request.CandidateId, request.Date);
            return result;
        }
    }
}
