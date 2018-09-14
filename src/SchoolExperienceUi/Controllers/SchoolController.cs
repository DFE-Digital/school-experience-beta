using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolExperienceBaseTypes;
using SchoolExperienceUi.Facades;
using SchoolExperienceUi.Models.School;

namespace SchoolExperienceUi.Controllers
{
    public class SchoolController : Controller
    {
        private static readonly string UserId = "33333333-3333-3333-3333-333333333333";
        private readonly ISchoolFacade _schoolService;

        private readonly IMapper _mapper;

        public SchoolController(ISchoolFacade schoolService, IMapper mapper)
        {
            _schoolService = schoolService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Diary));
        }

        [HttpGet]
        public IActionResult Diary()
        {
            var viewModel = new DiaryViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Events(GetEventsRequest request)
        {
            var result = await _schoolService.GetDiaryEntriesAsync(UserId, request.SchoolId, request.Start, request.End);
            return Ok(_mapper.Map<DiaryViewModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> BookCandidate([FromBody] Models.School.BookCandidateRequest request)
        {
            var result = await _schoolService.BookCandidate(UserId, request.SchoolId, request.CandidateId, request.When);

            var response = _mapper.Map<BookCandidateResponse>(result);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _schoolService.CancelBooking(UserId, id);
            var response = _mapper.Map<CancelBookingResponse>(result);

            return Ok(response);
        }
    }
}