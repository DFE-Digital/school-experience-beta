using System;

namespace SchoolExperienceSchoolUi
{
    public class DfeSignInOptions
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string MetaDataAddress { get; set; }
        public string CallbackPath { get; set; }
        public string SignedOutCallbackPath { get; set; }
        public string RedirectHostName { get; set; }
        public string TokenEndPoint { get; set; }
        public TimeSpan CookieTimeout { get; set; }
    }
}