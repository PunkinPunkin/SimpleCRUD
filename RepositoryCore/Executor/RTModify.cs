using LibServer.Repository;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;

namespace RepositoryCore.Executor
{
    #region Interface
    public interface IRTModify<R> : IRepositoryAction<List<R>, bool> where R : class
    {
        string UpdateFields { get; set; }
        string IgnoreFields { get; set; }
    }
    #endregion

    #region Class
    public class CRTModify<R> : ADbActionExecutor<List<R>, bool>, IRTModify<R> where R : class
    {
        public string IgnoreFields { get; set; }
        public string UpdateFields { get; set; }

        public override bool Execute(RetCode retCode, List<R> request)
        {
            if (request == null || request.Count == 0)
            {
                SetRetCode(retCode, CommonCode.NotChangeData);
                return true;
            }

            try
            {
                request.ForEach(o => { _unit.Repository<R>().Update(o); });
                SetRetCode(retCode, CommonCode.OK);
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
