using Newtonsoft.Json;

namespace LibreTranslate.Net.Enhanced.Models
{
    /// <summary>
    /// The model for the supported languages api
    /// </summary>
    public class SupportedLanguages
    {
        /// <summary>
        /// The code of the language
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// The english based language name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
