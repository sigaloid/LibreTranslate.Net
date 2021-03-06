# LibreTranslate.Net
## C# translation library
### Installation

Clone repo, build, add reference to project. Looking for Nuget maintainer, as I don't have a Microsoft account.

### Usage
```csharp
using System;
using LibreTranslate.Net;
...

Console.WriteLine("Hello World!");
var translate = new Translate();
string text = translate.TranslateText(Language.En, Language.Es, "Hello World!"); //English->Spanish
Console.WriteLine(text);
```
Output:
```
Hello World!
¡Hola Mundo!
```
Custom LibreTranslate URL (style: `http[s]://url` with no trailing `/`):
```csharp
var translate = new Translate("https://server_url");
```
### Language codes
Language|Struct
-|-
English|`Language.En`
Arabic|`Language.Ar`
Chinese|`Language.Zh`
French|`Language.Fr`
German|`Language.De`
Italian|`Language.It`
Portuguese|`Language.Pt`
Russian|`Language.Ru`
Spanish|`Language.Es`
