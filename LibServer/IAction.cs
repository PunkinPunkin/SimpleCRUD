using Shared.DTO;
using System;

namespace LibServer
{
    public interface IAction<R, T> : IDisposable, IComponent
    {
        T Execute(RetCode retCode, R request);
    }
}
