using BitPortLibrary.Objects;
using Microsoft.Extensions.Logging;

namespace BitPortLibrary.Auth;

public class UserCodeFileAuth: IAuth
{
    private readonly ILoggerFactory _factory;
    private UserCodeAuthObject _token = null;
    
    public UserCodeFileAuth(ILoggerFactory factory)
    {
        _factory = factory;
        if (File.Exists("token.json"))
        {
            _token = HelperMethod.DeserializeObjectFromFile<UserCodeAuthObject>("token.json");
        }
        else
        {
            throw new FileNotFoundException("Token file not found.");
        }
    }

    public async Task<UserCodeAuthObject> Authorize()
    {
        return _token;
    }

    public bool GetAuthorizationStatus()
    {
        return true;
    }

    public string GetToken()
    {
        return _token.access_token;
    }
}