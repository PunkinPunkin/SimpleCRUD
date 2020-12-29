using LibServer;
using LibServer.DataBase;
using LibServer.Repository;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryCore.Executor
{
    #region Interface
    /// <summary>
    /// 通用查詢Repository介面
    /// </summary>
    public interface IRTQuery<T> : IAction<Expression<Func<T, bool>>, IQueryable<T>> where T : class { }
    #endregion

    #region Class
    /// <summary>
    /// 通用查詢Repository
    /// </summary>
    public class CRTQuery<T> : ADbActionExecutor<Expression<Func<T, bool>> , IQueryable<T>>, IRTQuery<T> where T : class
    {
        public override IQueryable<T> Execute(RetCode retCode, Expression<Func<T, bool>> expression)
        {
            IQueryable<T> result = _unit.Repository<T>().Query(expression);
            SetRetCode(retCode, CommonCode.OK);
            return result;
        }
    }
    #endregion
}
