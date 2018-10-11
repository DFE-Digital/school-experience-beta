using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolExperienceSchoolUi.Pages.Components.SignInDetails
{
    public class SignInDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new ViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.FirstOrDefault(x => x.Type == "given_name");
                viewModel.Name = name.Value;
                viewModel.LastSignIn = DateTime.UtcNow.AddMinutes(-37);
            }
            else
            {
                viewModel.Name = "Unknown";
            }

            return await Task.FromResult(View(viewModel));
        }
    }
}
