using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace LibreTranslate.Net
{
    public class LibreTranslate
    {
        /// <summary>
        /// The http client
        /// </summary>
        private readonly HttpClient HttpClient;
        /// <summary>
        /// The default contructor. The default http client base uri points to https://libretranslate.com
        /// </summary>
        public LibreTranslate()
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://libretranslate.com")
            };
        }
        /// <summary>
        /// Contructor which enable to specified the libre translate server api address
        /// </summary>
        /// <param name="url"></param>
        public LibreTranslate(string url)
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
        }
        /// <summary>
        /// Gets the server supported languages.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SupportedLanguages>> GetSupportedLanguagesAsync()
        {
            return JsonConvert.DeserializeObject<IEnumerable<SupportedLanguages>>(await HttpClient.GetStringAsync("/languages"));
        }
        /// <summary>
        /// Translates the text from one language to another.
        /// </summary>
        /// <param name="fromLang"></param>
        /// <param name="toLang"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<string> TranslateAsync(Translate translate) => await TranslateAsync(translate, CancellationToken.None);
        /// <summary>
        /// Translates the text from one language to another. Can be cancelled by providing CancellationToken.
        /// </summary>
        /// <param name="fromLang"></param>
        /// <param name="toLang"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<string> TranslateAsync(Translate translate, CancellationToken cancellationToken)
        {
            var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", translate.Text },
                { "source", translate.Source.ToString() },
                { "target", translate.Target.ToString() },
                { "api_key", translate.ApiKey }
            });
            var response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/translate")
            {
                Content = formUrlEncodedContent
            },
            cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var translatedText = JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
                cancellationToken.ThrowIfCancellationRequested();
                return translatedText.TranslatedText;
            }
            cancellationToken.ThrowIfCancellationRequested();
            return default;
        }
    }
}