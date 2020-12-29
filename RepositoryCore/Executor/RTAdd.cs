using LibServer.Repository;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;

namespace RepositoryCore.Executor
{
    #region Interface
    /// <summary>
    /// Insert 資料表的Repository
    /// </summary>
    public interface IRTAdd<R> : IRepositoryAction<List<R>, bool> where R : class { }
    #endregion

    #region Class
    /// <summary>
    /// Insert 資料表的Repository
    /// </summary>
    public class CRTAdd<R> : ADbActionExecutor<List<R>, bool>, IRTAdd<R> where R : class
    {
        /// <summary>
        /// Insert 資料表的Repository
        /// </summary>
        /// <param name="retCode">RetCode 物件</param>
        /// <param name="request">要Insert的資料</param>
        /// <returns>Insert資料表結果</returns>
        public override bool Execute(RetCode retCode, List<R> request)
        {
            if (request == null || request.Count == 0)
            {
                SetRetCode(retCode, CommonCode.NotChangeData);
                return true;
            }

            try
            {
                request.ForEach(o => { _unit.Repository<R>().Create(o); });
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
