using LibreTranslate.Net.Enhanced.Constants;
using Newtonsoft.Json;

namespace LibreTranslate.Net.Enhanced.Models
{
    public class TranslateFile
    {
        [JsonProperty("file")] public string File { get; set; }
        [JsonProperty("source")] public LanguageCode Source { get; set; }
        [JsonProperty("target")] public LanguageCode Target { get; set; }
        [JsonProperty("api_key")] public string ApiKey { get; set; }
    }
}