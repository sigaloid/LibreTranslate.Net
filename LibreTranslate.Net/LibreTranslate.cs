using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private readonly string ApiKey;
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
        public LibreTranslate(string url, string apiKey = null)
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            ApiKey = apiKey;
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
        public async Task<string> TranslateAsync(Translate translate)
        {
            translate.ApiKey = string.IsNullOrWhiteSpace(translate.ApiKey) ? ApiKey : translate.ApiKey;
            var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", translate.Text },
                { "source", translate.Source.ToString() },
                { "target", translate.Target.ToString() },
                { "api_key", translate.ApiKey },
                { "format", translate.Format?.ToString() }
            });
            var response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/translate")
            {
                Content = formUrlEncodedContent
            });
            if (response.IsSuccessStatusCode)
            {
                var translatedText = JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
                return translatedText.TranslatedText;
            }
            return default;
        }

        public async Task<List<DetectResponse>> DetectAsync(Detect detect)
        {
            var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", detect.Text },
                { "api_key", detect.ApiKey }
            });
            var response = await HttpClient.PostAsync("/detect", formUrlEncodedContent);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var detectResponse =
                JsonConvert.DeserializeObject<List<DetectResponse>>(await response.Content.ReadAsStringAsync());
            return detectResponse;
        }

        public async Task<string> DetectAsStringAsync(Detect detect)
        {
            var response = await DetectAsync(detect);
            return response.FirstOrDefault()?.Language;
        }

        public async Task<TranslationResponse> TranslateFileAsync(TranslateFile translateFile)
        {
            
            var fileBytes = File.ReadAllBytes(translateFile.File);
            var fileContent = new ByteArrayContent(fileBytes)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("text/plain"),
                    ContentDisposition = ContentDispositionHeaderValue.Parse($"form-data; name=\"file\"; filename=\"{translateFile.File}\"")
                }
            };
            var multipart = new MultipartFormDataContent();
            multipart.Add(new StringContent(translateFile.Source.ToString()), "source");
            multipart.Add(new StringContent(translateFile.Target.ToString()), "target");
            multipart.Add(new StringContent(translateFile.ApiKey), "api_key");
            multipart.Add(fileContent);
            
            var response = await HttpClient.PostAsync("/translate_file", multipart);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
            }

            return default;
        }

        public async Task<FrontendSettingsResponse> FrontendSettingsAsync()
        {
            var response = await HttpClient.GetAsync("/frontend/settings");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<FrontendSettingsResponse>(
                    await response.Content.ReadAsStringAsync());
            }

            return default;
        }

        public async Task<bool> SuggestAsync(Suggestion suggestion)
        {
            var urlEncoded = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", suggestion.SourceText },
                { "s", suggestion.TargetText },
                { "source", suggestion.Source },
                { "target", suggestion.Target }
            });
            var response = await HttpClient.PostAsync("/suggest", urlEncoded);
            var result = JsonConvert.DeserializeObject<SuggestionResponse>(await response.Content.ReadAsStringAsync());
            return result.Success;
        }
    }
}