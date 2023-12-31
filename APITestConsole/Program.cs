using Autofac;
using BitPortLibrary;
using BitPortLibrary.Auth;
using BitPortLibrary.Objects.Cloud;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

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
var folders = (await client.ByPath("")).data;

ByPathObject.Folders s = null;
foreach (var data in folders)
{
    foreach (var folder in data.folders)
    {
        Console.WriteLine(folder.code + " -> " + folder.name + " -> " + folder.code);
        s = folder;
    }
}

await client.GetFolderZipURL(s.code);
var added = await client.AddMagnet("magnet:?xt=urn:btih:3F921FBA057A9A78419558149F6D16FF8033711B&dn=Beautiful+C%2B%2B%3A+30+Core+Guidelines+for+Writing+Clean%2C+Safe%2C+and+Fast+Code+%28True+EPUB%2C+MOBI%29&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337%2Fannounce&tr=udp%3A%2F%2Fipv4.tracker.harry.lu%3A80%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2710%2Fannounce&tr=udp%3A%2F%2F9.rarbg.com%3A2710%2Fannounce&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969%2Fannounce&tr=udp%3A%2F%2Fexplodie.org%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80%2Fannounce&tr=udp%3A%2F%2Ftracker.tiny-vps.com%3A6969%2Fannounce&tr=udp%3A%2F%2Fexodus.desync.com%3A6969%2Fannounce&tr=udp%3A%2F%2Fopen.stealth.si%3A80%2Fannounce&tr=udp%3A%2F%2Ftracker.torrent.eu.org%3A451%2Fannounc&tr=udp%3A%2F%2F9.rarbg.to%3A2710%2Fannounce&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337%2Fannounce&tr=http%3A%2F%2Ftracker.openbittorrent.com%3A80%2Fannounce&tr=udp%3A%2F%2Fopentracker.i2p.rocks%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.internetwarriors.net%3A1337%2Fannounce&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969%2Fannounce&tr=udp%3A%2F%2Fcoppersurfer.tk%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.zer0day.to%3A1337%2Fannounce");
if (added)
{
    Console.WriteLine("Torrent added.");
}

var transfers = await client.GetTransfers();
Console.WriteLine(transfers);