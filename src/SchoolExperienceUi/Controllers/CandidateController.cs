using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolExperienceBaseTypes;
using SchoolExperienceUi.Facades;
using SchoolExperienceUi.Models.Candidate;

namespace SchoolExperienceUi.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ISchoolFacade _schoolService;
        private readonly ICandidateFacade _candidateService;

        private readonly IMapper _mapper;

        private static readonly Guid UserId = new Guid("11111111-1111-1111-1111-111111111111");

        public CandidateController(IMapper mapper, ISchoolFacade schoolService, ICandidateFacade candidateFacade)
        {
            _schoolService = schoolService;
            _candidateService = candidateFacade;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Bookings = new List<IndexBookingItem>(),
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult FindSchools()
        {
            var viewModel = new FindSchoolViewModel
            {
                PostCode = "OL15 8RE",
                SearchDistanceInMiles = 1,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FindSchools(FindSchoolViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var schools = await _schoolService.FindSchoolsAsync(viewModel.PostCode, new Distance { Miles = viewModel.SearchDistanceInMiles });
                viewModel.Schools = _mapper.Map<IEnumerable<FindSchoolViewModel.School>>(schools.Schools);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Diary()
        {
            var viewModel = new DiaryViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Events(DateTime? start, DateTime? end)
        {
            var today = DateTime.UtcNow.Date;
            if (!start.HasValue)
            {
                start = today.StartOfMonth();
            }
            if (!end.HasValue)
            {
                end = today.EndOfMonth();
            }

            var result = await _candidateService.GetDiaryEntriesAsync(UserId, start.Value, end.Value);
            return Ok(_mapper.Map<DiaryViewModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiaryEntry([FromBody] CreateDiaryEntryRequest request)
        {
            var result = await _candidateService.CreateDiaryEntryAsync(UserId, request.Start, request.End, DiaryEntryType.Free);

            var viewModel = _mapper.Map<CreateDiaryEventViewModel>(result);

            return Ok(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveDiaryEntry(int id)
        {
            var result = await _candidateService.DeleteDiaryEntryAsync(UserId, id);

            return Ok(result.Result);
        }
    }
}
