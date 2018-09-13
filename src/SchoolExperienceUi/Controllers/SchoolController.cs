using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolExperienceApiDto.School;
using SchoolExperienceUi.Facades;

namespace SchoolExperienceUi.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolFacade _schoolService;

        public SchoolController(ISchoolFacade schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<FindSchoolsResult> FindSchools(FindSchoolsRequest request)
        {
            var result = await _schoolService.FindSchoolsAsync(request.PostCode, request.Distance);
            return result;
        }
    }
}