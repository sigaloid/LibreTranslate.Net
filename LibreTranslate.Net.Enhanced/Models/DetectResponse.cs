using Newtonsoft.Json;

namespace LibreTranslate.Net.Enhanced.Models
{
    public class DetectResponse
    {
        [JsonProperty("confidence")]
        public double? Confidence { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}