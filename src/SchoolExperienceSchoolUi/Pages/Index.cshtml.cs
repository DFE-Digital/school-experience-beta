using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolExperienceSchoolUi.Facades;

namespace SchoolExperienceSchoolUi.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ISchoolFacade _schoolFacade;

        private readonly IMapper _mapper;

        public DashboardModel(ISchoolFacade schoolFacade, IMapper mapper)
        {
            _schoolFacade = schoolFacade;
            _mapper = mapper;
        }

        public int TotalAdvertised { get; set; }
        public int TotalBooked { get; set; }
        public int TotalAvailable { get; set; }

        public class SubjectPlacement
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public class SchoolPlacement
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public IEnumerable<SubjectPlacement> SubjectPlacements { get; set; }
        public IEnumerable<SchoolPlacement> SchoolPlacements { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var stats = await _schoolFacade.GetPlacementTotals();
                TotalAdvertised = stats.TotalAdvertised;
                TotalAvailable = stats.TotalAvailable;
                TotalBooked = stats.TotalBooked;
                SubjectPlacements = stats.SubjectPlacements.Select(x => new SubjectPlacement { Name = x.Name, Count = x.Count });
                SchoolPlacements = stats.SchoolPlacements.Select(x => new SchoolPlacement { Name = x.Name, Count = x.Count });
            }
            catch(HttpRequestException ex)
            {
                TotalAdvertised = TotalAvailable = TotalBooked = -1;
                SubjectPlacements = new List<SubjectPlacement>();
                SchoolPlacements = new List<SchoolPlacement>();
            }
        }
    }
}