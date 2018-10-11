using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolExperienceSchoolUi.Pages.Auth
{
    [Authorize]
    public class SignedInModel : PageModel
    {
        public Task OnPostAsync()
        {
            return Task.FromResult(Redirect("/Association"));
        }
    }
}