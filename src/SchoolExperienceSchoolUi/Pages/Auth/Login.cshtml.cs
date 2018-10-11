using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolExperienceSchoolUi.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync(string returnUrl)
        {
            await HttpContext.ChallengeAsync(new AuthenticationProperties() { RedirectUri = returnUrl });
        }
    }
}