using BitPortLibrary.Objects;

namespace BitPortLibrary.Auth;

public interface IAuth
{
    public Task<UserCodeAuthObject> Authorize();
    public bool GetAuthorizationStatus();
    public string GetToken();
}