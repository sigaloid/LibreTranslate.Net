using Newtonsoft.Json;

namespace LibreTranslate.Net
{
    public class Suggestion
    {
        [JsonProperty("q")] public string SourceText { get; set; }
        [JsonProperty("s")] public string TargetText { get; set; }
        [JsonProperty("source")] public string Source { get; set; }
        [JsonProperty("target")] public string Target { get; set; }
        
    }
}