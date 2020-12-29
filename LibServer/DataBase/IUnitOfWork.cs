using LibServer.Repository;
using System;
using log4net;

namespace LibServer.DataBase
{
    /// <summary>
    /// 實作Unit Of Work的interface。
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 撰寫 Log 物件
        /// </summary>
        ILog Logger { get; set; }

        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        void Commit();

        void Rollback();

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        IRepository<T> Repository<T>() where T : class;
    }
}
