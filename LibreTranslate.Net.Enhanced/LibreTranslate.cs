using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LibreTranslate.Net.Enhanced.Models;
using Newtonsoft.Json;
namespace LibreTranslate.Net.Enhanced
{
    public class LibreTranslate : IDisposable
    {
        /// <summary>
        /// The http client
        /// </summary>
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;
        /// <summary>
        /// The default contructor. The default http client base uri points to https://libretranslate.com
        /// </summary>
        public LibreTranslate()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://libretranslate.com")
            };
        }

        /// <summary>
        /// Contructor which enable to specified the libre translate server api address
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiKey"></param>
        public LibreTranslate(string url, string apiKey = null)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            _apiKey = apiKey;
        }
        /// <summary>
        /// Gets the server supported languages.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SupportedLanguages>> GetSupportedLanguagesAsync()
        {
            return JsonConvert.DeserializeObject<IEnumerable<SupportedLanguages>>(await _httpClient.GetStringAsync("/languages"));
        }

        /// <summary>
        /// Translates the text from one language to another.
        /// </summary>
        /// <param name="translate">Payload for the request</param>
        /// <returns></returns>
        public async Task<string> TranslateAsync(Translate translate)
        {
            translate.ApiKey = string.IsNullOrWhiteSpace(translate.ApiKey) ? _apiKey : translate.ApiKey;
            var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", translate.Text },
                { "source", translate.Source.ToString() },
                { "target", translate.Target.ToString() },
                { "api_key", translate.ApiKey },
                { "format", translate.Format?.ToString() }
            });
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/translate")
            {
                Content = formUrlEncodedContent
            });
            var translatedText = JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                
                return translatedText.TranslatedText;
            }
            
            return translatedText.Error;
        }

        public async Task<List<DetectResponse>> DetectAsync(Detect detect)
        {
            var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", detect.Text },
                { "api_key", detect.ApiKey }
            });
            var response = await _httpClient.PostAsync("/detect", formUrlEncodedContent);
            var detectResponse = new List<DetectResponse>();
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest)
            {
                detectResponse = JsonConvert.DeserializeObject<List<DetectResponse>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                var errorResponse = new DetectResponse()
                {
                    Error = $"An unknown error has occurred: {errorMessage}"
                };
                detectResponse.Add(errorResponse);
            }

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
            
            var response = await _httpClient.PostAsync("/translate_file", multipart);
            var allowedStatusCodes = new int[]
            {
                (int)HttpStatusCode.BadRequest,
                (int)HttpStatusCode.Forbidden,
                429, // TooManyRequests is not supported in .NET Standard 2.0,
                (int)HttpStatusCode.InternalServerError
            };
            if (response.IsSuccessStatusCode || allowedStatusCodes.Contains((int)response.StatusCode))
            {
                return JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return new TranslationResponse()
            {
                Error = $"Unknown error {errorMessage}"
            };
        }

        public async Task<FrontendSettingsResponse> FrontendSettingsAsync()
        {
            var response = await _httpClient.GetAsync("/frontend/settings");
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
            var response = await _httpClient.PostAsync("/suggest", urlEncoded);
            var result = JsonConvert.DeserializeObject<SuggestionResponse>(await response.Content.ReadAsStringAsync());
            return result.Success;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}