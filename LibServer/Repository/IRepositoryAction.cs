using System.Collections.Generic;

namespace LibServer.Repository
{
    /// <summary>
    /// 查詢 Repository 介面
    /// </summary>
    /// <typeparam name="R">Input物件</typeparam>
    /// <typeparam name="T">Output物件</typeparam>
    public interface IRepositoryAction<R, T> : IAction<R, T>
    {
    }

    /// <summary>
    /// Insert/Update資料表的Repository
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public interface IRModifyTable<R> : IRepositoryAction<List<R>, int>
    {
        string UpdateFields { get; set; }
        string IgnoreFields { get; set; }
    }
}
