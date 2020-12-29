using LibServer.Repository;
using log4net;
using Shared;
using System;
using System.Collections;
using System.Data.Entity;

namespace LibServer.DataBase
{
    /// <summary>
    /// 實作Entity Framework Unit Of Work的class
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private EnvType _environment;
        private readonly DbContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        /// <param name="context">設定UOF的context</param>
        public UnitOfWork(DbContext context, string environment)
        {
            _environment = (EnvType)Enum.Parse(typeof(EnvType), environment);
            _context = context;
        }

        /// <summary>
        /// 撰寫 Log 物件
        /// </summary>
        public ILog Logger { get; set; }

        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //EF6 no need to rollback
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        /// <param name="disposing">是否在清理中？</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}
