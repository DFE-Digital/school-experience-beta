using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceServices.PerformancePlatform
{
    public class PerformancePlatformOptions
    {
        /// <summary>
        /// Gets or sets the write host.  
        /// </summary>
        /// <value>
        /// The URL of the performance service, typically 'https://www.performance.service.gov.uk'.
        /// </value>
        public string WriteHost { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The 'Data-group' this service is posting to.  Probably 'school-experience'.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets the bearer token.
        /// </summary>
        /// <value>
        /// Assigned by the performance platform team.
        /// </value>
        public string BearerToken { get; set; }
    }
}
