using Newtonsoft.Json;
namespace LibreTranslate.Net
{
    /// <summary>
    /// The model to send to the libre translate api
    /// </summary>
    public class Translate
    {
        /// <summary>
        /// The text to be translated
        /// </summary>
        [JsonProperty("q")]
        public string Q { get; set; }
        /// <summary>
        /// The source of the current language text
        /// </summary>
        [JsonProperty("source")]
        public LanguageCode Source { get; set; }
        /// <summary>
        /// The target of the language we want to convert text
        /// </summary>
        [JsonProperty("target")]
        public LanguageCode Target { get; set; }
    }
}
