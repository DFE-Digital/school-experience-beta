using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceCandidateDataServices.Implementation
{
    public class GitisServicesOptions
    {
        /// <summary>
        /// Gets or sets the URI of the Gitis service.
        /// </summary>
        public Uri ServiceUri { get; set; }
        public string UserName { get; internal set; }
        public string Password { get; internal set; }
    }
}
