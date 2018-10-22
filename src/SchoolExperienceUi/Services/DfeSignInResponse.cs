using Newtonsoft.Json;

namespace SchoolExperienceUi.Services
{
    internal class DfeSignInResponse
    {
        /// <summary>
        /// Gets or sets the sourceId used in original request.
        /// </summary>
        [JsonProperty(PropertyName = "sourceId")]
        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets the DfE Sign-in identifier for user.
        /// </summary>
        [JsonProperty(PropertyName = "sub")]
        public string Sub { get; set; }
    }
}
