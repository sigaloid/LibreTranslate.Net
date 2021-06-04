# LibreTranslate.Net
## C# translation library using LibreTranslate for .Net
<p>
	<a href="https://www.nuget.org/packages/LibreTranslate.Net">
	    <img src="https://buildstats.info/nuget/LibreTranslate.Net?v=1.0.0" />
	</a>
</p>

### Installation
`Install-Package LibreTranslate.Net -Version 1.0.0`
### Using
```csharp
using LibreTranslate.Net;
```
### Usage
```csharp
var LibreTranslate = new LibreTranslate();
System.Collections.Generic.IEnumerable<SupportedLanguages> SupportedLanguages = await LibreTranslate.GetSupportedLanguagesAsync();
System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(supportedLanguages, Newtonsoft.Json.Formatting.Indented));
var englishText = "Hello World!";
string spanishText = await LibreTranslate.TranslateAsync(new Translate() {
    ApiKey = "MySecretApiKey",
    Source = LanguageCode.English,
    Target = LanguageCode.Spanish,
    Text = englishText
});
System.Console.WriteLine(spanishText);
```
### Output:
```
Hello World!
¡Hola Mundo!
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
Chinese|`LanguageCode.Chinese`
French|`LanguageCode.French`
German|`LanguageCode.German`
Hindi|`LanguageCode.Hindi`
Irish|`LanguageCode.Irish`
Italian|`LanguageCode.Italian`
Japanese|`LanguageCode.Japanese`
Korean|`LanguageCode.Korean`
Portuguese|`LanguageCode.Portuguese`
Russian|`LanguageCode.Russian`
Spanish|`LanguageCode.Spanish`
AutoDetect|`LanguageCode.AutoDetect //This feature is experimental`
