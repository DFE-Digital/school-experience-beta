using System;

namespace SchoolExperienceUi.Services
{
    public class DfePublicSignOnOptions
    {
        public Uri ServiceUrl { get; set; }
        public Uri CallbackUrl { get; set; }
        public Uri UserRedirect { get; set; }
        public string Organisation { get; set; }
    }
}