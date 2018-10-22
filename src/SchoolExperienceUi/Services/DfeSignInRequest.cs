using System;
using Newtonsoft.Json;

namespace SchoolExperienceUi.Services
{
    internal class DfeSignInRequest
    {
        /// <summary>
        /// Gets or sets the identifier of the user in your system. Will be included in back channel response.
        /// </summary>
        [JsonProperty(PropertyName = "sourceId")]
        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets the users given name.
        /// </summary>
        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the user family name.
        /// </summary>
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user. This is also a unique identifier of a user in DfE Sign-in.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the DfE Sign-in identifier that the user should be associated to.
        /// </summary>
        [JsonProperty(PropertyName = "organisation")]
        public string Organisation { get; set; }

        /// <summary>
        /// Gets or sets the URL that the back channel response should be sent to. See details of back channel response below.
        /// </summary>
        [JsonProperty(PropertyName = "callback")]
        public Uri Callback { get; set; }

        /// <summary>
        /// Gets or sets the URL that a user, if going through the onboarding, should be returned to upon completion.
        /// If omitted, the default redirect for your client will be used.
        /// </summary>
        [JsonProperty(PropertyName = "userRedirect")]
        public Uri UserRedirect { get; set; }

        /// <summary>
        /// Gets or sets the subject of the invitation email.
        /// </summary>
        [JsonProperty(PropertyName = "inviteSubjectOverride")]
        public string InviteSubjectOverride { get; set; }

        /// <summary>
        /// Gets or sets the content of the invitation email.
        /// </summary>
        [JsonProperty(PropertyName = "inviteBodyOverride")]
        public string InviteBodyOverride { get; set; }
    }
}

