using System;
using Newtonsoft.Json;

namespace SchoolExperienceServices.PerformancePlatform
{
    public class PerformancePlatformMetric
    {
        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_timestamp")]
        public DateTime When { get; set; }
    }
}
