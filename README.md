# LibreTranslate.Net
## C# translation library using LibreTranslate for .Net
<p>
	<a href="https://www.nuget.org/packages/LibreTranslate.Net">
	    <img src="https://buildstats.info/nuget/LibreTranslate.Net?v=1.0.1" />
	</a>
</p>

### Installation
`Install-Package LibreTranslate.Net -Version 1.0.1`
### Using
```csharp
using LibreTranslate.Net;
```
### Usage
```csharp
var libreTranslate = new LibreTranslate();

//Get supported languages
System.Collections.Generic.IEnumerable<SupportedLanguages> SupportedLanguages = await libreTranslate.GetSupportedLanguagesAsync();
System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(supportedLanguages, Newtonsoft.Json.Formatting.Indented));

//text translation
var englishText = "Hello World!";
string spanishText = await libreTranslate.TranslateAsync(new Translate() {
    ApiKey = "MySecretApiKey",
    Source = LanguageCode.English,
    Target = LanguageCode.Spanish,
    Text = englishText
});
System.Console.WriteLine(spanishText);

//html translation
var englishHtml = "<p>Hello World!</p>";
string spanishHtml = await libreTranslate.TranslateAsync(new Translate() {
    ApiKey = "MySecretApiKey",
    Source = LanguageCode.English,
    Target = LanguageCode.Spanish,
    Format = Format.HTML,
    Text = englishHtml
});
System.Console.WriteLine(spanishHtml);
```
### Output:
```
Hello World!
¡Hola Mundo!
<p>¡Hola Mundo!</p>
```
### Custom LibreTranslate URL (style: `http[s]://url` with no trailing `/`):
```csharp
var LibreTranslate = new LibreTranslate("https://server_url");
```
### LibreTranslate Methods
```csharp
Task<IEnumerable<SupportedLanguages>> GetSupportedLanguagesAsync();
Task<string> TranslateAsync(Translate translate);
```
### Language codes
Language|Code
-|-
English|`LanguageCode.English`
Arabic|`LanguageCode.Arabic`
Azerbaijani|`LanguageCode.Azerbaijani`
Catalan|`LanguageCode.Catalan`
Chinese|`LanguageCode.Chinese`
Czech|`LanguageCode.Czech`
Danish|`LanguageCode.Danish`
Dutch|`LanguageCode.Dutch`
Esperanto|`LanguageCode.Esperanto`
Finnish|`LanguageCode.Finnish`
French|`LanguageCode.French`
German|`LanguageCode.German`
Greek|`LanguageCode.Greek`
Hebrew|`LanguageCode.Hebrew`
Hindi|`LanguageCode.Hindi`
Hungarian|`LanguageCode.Hungarian`
Indonesian|`LanguageCode.Indonesian`
Irish|`LanguageCode.Irish`
Italian|`LanguageCode.Italian`
Japanese|`LanguageCode.Japanese`
Korean|`LanguageCode.Korean`
Persian|`LanguageCode.Persian`
Polish|`LanguageCode.Polish`
Portuguese|`LanguageCode.Portuguese`
Russian|`LanguageCode.Russian`
Slovak|`LanguageCode.Slovak`
Spanish|`LanguageCode.Spanish`
Swedish|`LanguageCode.Swedish`
Turkish|`LanguageCode.Turkish`
Ukranian|`LanguageCode.Ukranian`
AutoDetect|`LanguageCode.AutoDetect //This feature is experimental`
