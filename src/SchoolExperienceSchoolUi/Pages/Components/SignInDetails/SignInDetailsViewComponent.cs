using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolExperienceSchoolUi.Pages.Components.SignInDetails
{
    public class SignInDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new ViewModel()
            {
                Name = "Neil Scales",
                LastSignIn = DateTime.UtcNow.AddMinutes(-37)
            };

            return View(viewModel);
        }
    }
}
