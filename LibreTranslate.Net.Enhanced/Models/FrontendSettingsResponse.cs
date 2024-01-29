using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibreTranslate.Net.Enhanced.Models
{
    public class FrontendSettingsResponse
    {
        [JsonProperty("api_keys")]
        public bool ApiKeys { get; set; }

        [JsonProperty("charLimit")]
        public int? CharLimit { get; set; }
        
        [JsonProperty("frontendTimeout")] 
        public int? FrontendTimeout { get; set; }
        [JsonProperty("keyRequired")]
        public bool KeyRequired { get; set; }

        [JsonProperty("language")]
        public SupportedLanguages Language { get; set; }

        [JsonProperty("suggestions")]
        public bool Suggestions { get; set; }

        [JsonProperty("supportedFilesFormat")]
        public List<string> SupportedFilesFormat { get; set; }
    }
}