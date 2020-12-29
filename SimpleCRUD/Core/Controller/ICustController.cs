using Shared;

namespace SimpleCRUD.Core.Controller
{
    public interface ICustController
    {
        EnvType Env { get; }

        string ClientIp { get; }
    }
}