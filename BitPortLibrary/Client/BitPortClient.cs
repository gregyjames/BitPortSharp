using BitPortLibrary.Auth;

namespace BitPortLibrary;

public class BitPortClient
{
    internal HttpClient _client = null;
    public BitPortClient(IAuth auth)
    {
        if (!auth.GetAuthorizationStatus())
        {
            throw new Exception("Autherize before proceeding.");
        }

        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("Authorization", auth.GetToken());
    }
}