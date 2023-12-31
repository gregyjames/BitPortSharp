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
