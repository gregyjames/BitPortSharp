using Autofac;
using BitPortLibrary.Auth;

namespace BitPortLibrary;

public class AutoFacModule: Module
{
    private readonly AuthorizationTypes _type;

    public AutoFacModule(AuthorizationTypes type)
    {
        _type = type;
    }

    protected override void Load(ContainerBuilder builder)
    {
        switch (_type)
        {
            case AuthorizationTypes.USER_CODE_AUTH:
                builder.RegisterType<UserCodeAuth>().As<IAuth>();
                break;
            case AuthorizationTypes.USER_CODE_FILE_AUTH:
                builder.RegisterType<UserCodeFileAuth>().As<IAuth>();
                break;
            default:
                break;
        }

        builder.RegisterType<BitPortClient>();
    }
}