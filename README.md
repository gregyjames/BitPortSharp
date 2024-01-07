[![.NET](https://github.com/gregyjames/BitPortSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/gregyjames/BitPortSharp/actions/workflows/dotnet.yml)
[![NuGet latest version](https://badgen.net/nuget/v/BitPortSharp)](https://www.nuget.org/packages/BitPortSharp)

# BitPortSharp
C# Wrapper for the BitPort API.

## Example Usage
```csharp
var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");
var configuration = configurationBuilder.Build();

var seriLog = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Sixteen))
    .CreateLogger();
var factory = LoggerFactory.Create(logging =>
{
    logging.AddSerilog(seriLog);
});

var builder = new ContainerBuilder();
builder.RegisterInstance(configuration).As<IConfiguration>();
builder.RegisterInstance(factory).As<ILoggerFactory>();
builder.RegisterModule(File.Exists("token.json")
    ? new AutoFacModule(AuthorizationTypes.USER_CODE_FILE_AUTH)
    : new AutoFacModule(AuthorizationTypes.USER_CODE_AUTH));
var ctx = builder.Build();
var client = ctx.Resolve<BitPortClient>();
```

## Endpoints supported
 - [ ] Cloud Folders
	- [x] /v2/cloud/byPath
	- [ ] /v2/cloud/deletes
	- [ ] /v2/cloud/[<folderCode>]
	- [x] /v2/cloud/[<folderCode>]/download-as-zip
	- [ ] /v2/cloud/<folderCode>
	- [ ] /v2/cloud/item/byPath
	- [ ] /v2/cloud/paused-videos
	- [ ] /v2/cloud/all-videos
	- [ ] /v2/cloud/paused-videos
- [ ] Files
	- [ ] /v2/files/byPath
	- [ ] /v2/files/<fileCode>
	- [ ] /v2/files/deletes
	- [x] /v2/files/<fileCode>/download
	- [ ] /v2/files/<fileCode>/stream
	- [ ] /v2/files/<fileCode>/stream-url
	- [ ] /v2/files/<fileCode>/stream/information
	- [ ] /v2/files/<fileCode>/subtitles
	- [ ] /v2/files/<fileCode>/subtitles/<key>
	- [ ] /v2/files/<fileCode>
	- [ ] /v2/files/<fileCode>/media.m3u8
	- [ ] /v2/files/<fileCode>/stream.m3u8
	- [ ] /v2/files/<fileCode>/sub/<subtitlesLangCode>/<subtitlesId>.m3u8
	- [ ] /v2/files/<fileCode>/sub/<subtitlesLangCode>/<subtitlesId>.webvtt
- [ ] Me
	- [ ] /v2/me
- [ ] Search in cloud
	- [ ] /v2/search/<term>
- [ ] Transfers
	- [x] /v2/transfers
	- [ ] /v2/transfers/<token>

## License
MIT License

Copyright (c) 2023 Greg James

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
