using System.Text.Json;
using BitPortLibrary.Objects;
using Microsoft.Extensions.Configuration;

namespace BitPortLibrary.Auth;

public class UserCodeAuth: IAuth
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _userCode;
    private const string grant_type = "code";
    private HttpClient _client = new HttpClient();
    private bool Autherized = false;
    public UserCodeAuthObject token = null;
    private AuthorizationSettings _authorizationSettings = new AuthorizationSettings();
    public UserCodeAuth(string client_id, string client_secret, string user_code)
    {
        _clientId = client_id;
        _clientSecret = client_secret;
        _userCode = user_code;
    }

    public UserCodeAuth(IConfiguration config)
    {
        config.Bind(_authorizationSettings);
        _clientId = _authorizationSettings.clientId;
        _clientSecret = _authorizationSettings.clientSecret;
        _userCode = _authorizationSettings.userCode;
        Authorize().Wait();
    }
    public async Task<UserCodeAuthObject> Authorize()
    {
        var uri = new Uri(URLs.user_code_auth_address);
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

        var formData = new Dictionary<string, string>
        {
            { "client_id", _clientId},
            { "client_secret", _clientSecret },
            { "grant_type", grant_type },
            { "code", _userCode },
        };
        var content = new FormUrlEncodedContent(formData);
        var responseMessage = await _client.PostAsync(uri, content);
        var json = await responseMessage.Content.ReadAsStringAsync();
        if (responseMessage.IsSuccessStatusCode)
        {
            var json_object = JsonSerializer.Deserialize<UserCodeAuthObject>(json);
            token = json_object;
            HelperMethod.SerializeObjectToFile(json_object, "token.json");
            Autherized = true;
            return json_object;
        }
        else
        {
            Autherized = false;
            return null;
        }
    }

    public bool GetAuthorizationStatus()
    {
        return Autherized;
    }

    public string GetToken()
    {
        if (Autherized)
        {
            return token.access_token;
        }

        return string.Empty;
    }
}