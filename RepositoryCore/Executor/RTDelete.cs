using LibServer.Repository;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryCore.Executor
{
    #region Interface
    public interface IRTDelete<R> : IRepositoryAction<IEnumerable<R>, bool> where R : class { }
    #endregion

    #region Class
    public class CRTDelete<R> : ADbActionExecutor<IEnumerable<R>, bool>, IRTDelete<R> where R : class
    {
        public override bool Execute(RetCode retCode, IEnumerable<R> request)
        {
            if (request == null || !request.Any())
            {
                SetRetCode(retCode, CommonCode.NotChangeData);
                return true;
            }

            try
            {
                foreach (R r in request)
                    _unit.Repository<R>().Delete(r);
                return true;
            }
            catch (Exception ex)
            {
                SetRetCode(retCode, CommonCode.Fail, new string[] { ex.Message });
            }
            return false;
        }
    }
    #endregion
}
