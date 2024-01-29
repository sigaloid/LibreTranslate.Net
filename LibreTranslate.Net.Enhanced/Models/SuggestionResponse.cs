﻿using Newtonsoft.Json;

namespace LibreTranslate.Net.Enhanced.Models
{
    public class SuggestionResponse
    {
        internal SuggestionResponse(){}
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}