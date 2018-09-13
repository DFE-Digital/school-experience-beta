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
        public async Task<FindSchoolsResult> FindSchools(string postCode, int distance)
        {
            var result = await _schoolService.FindSchoolsAsync(postCode, new Distance { Metres = distance });
            return result;
        }
    }
}
