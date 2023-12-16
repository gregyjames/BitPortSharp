using BitPortLibrary.Auth;
using Microsoft.Extensions.Logging;

namespace BitPortLibrary;

public class BitPortClient
{
    internal HttpClient _client = null;
    internal ILogger<BitPortClient> _logger = null;
    public BitPortClient(IAuth auth, ILoggerFactory factory)
    {
        if (!auth.GetAuthorizationStatus())
        {
            throw new Exception("Autherize before proceeding.");
        }

        _logger = factory.CreateLogger<BitPortClient>();
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("Authorization", auth.GetToken());
    }
}