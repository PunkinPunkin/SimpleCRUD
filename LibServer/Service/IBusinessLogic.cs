using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServer.Service
{
    public interface IBusinessLogic<R, T> : IAction<R, T>
    {
    }
}
