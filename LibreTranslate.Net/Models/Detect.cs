using Newtonsoft.Json;

namespace LibreTranslate.Net
{
    public class Detect
    {
        [JsonProperty("q")] 
        public string Text { get; set; }
        
        [JsonProperty("api_key")] 
        public string ApiKey { get; set; }
    }
}