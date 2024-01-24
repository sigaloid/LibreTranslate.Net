using Newtonsoft.Json;

namespace LibreTranslate.Net
{
    public class TranslateFile
    {
        [JsonProperty("file")] public string File { get; set; }
        [JsonProperty("source")] public LanguageCode Source { get; set; }
        [JsonProperty("target")] public LanguageCode Target { get; set; }
        [JsonProperty("api_key")] public string ApiKey { get; set; }
    }
}