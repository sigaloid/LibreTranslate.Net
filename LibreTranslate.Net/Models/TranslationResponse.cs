using Newtonsoft.Json;
namespace LibreTranslate.Net
{
    /// <summary>
    /// The model for the translation api response
    /// </summary>
    public class TranslationResponse
    {
        /// <summary>
        /// The translated text
        /// </summary>
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
    }
}
