using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LibServer.Repository
{
    /// <summary>
    /// 實作Entity Framework Generic Repository 的 Class。
    /// </summary>
    /// <typeparam name="T">EF Model 裡面的Type</typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> _dbSet;
        protected DbContext Context { get; set; }

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
        /// </summary>
        /// <param name="inContext">Entity所在的Context</param>
        public Repository(DbContext dbContext)
        {
            Context = dbContext;
            _dbSet = Context.Set<T>();
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (!_dbSet.Local.Contains(entity) || entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void Update(T entity, Expression<Func<T, object>>[] updateProperties)
        {
            Context.Configuration.ValidateOnSaveEnabled = false;

            Context.Entry(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                foreach (var property in updateProperties)
                {
                    Context.Entry(entity).Property(property).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 儲存異動。
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (Context.Configuration.ValidateOnSaveEnabled == false)
            {
                Context.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /// <summary>
        /// 用主鍵查詢。
        /// </summary>
        /// <param name="keys">Key</param>
        /// <returns>取得符合條件的內容。</returns>
        public virtual T Get(object keys)
        {
            return _dbSet.Find(keys);
        }

        /// <summary>
        /// 取得全部的內容。
        /// </summary>
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// 取得符合條件的內容。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得符合條件的內容。</returns>
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                return _dbSet.AsQueryable().Where(expression);
            else
                return _dbSet.AsQueryable();
        }

        public virtual PaginatedList<T> GetPaged(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int pageIndex = 1, int pageSize = 10,
            Expression<Func<T, bool>> filter = null,
            IList<Expression<Func<T, object>>> includedProperties = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (filter != null)
                query = query.Where(filter);
            foreach (var includeProperty in includedProperties)
            {
                query = query.Include(includeProperty);
            }
            query = orderBy(query);
            int totalCount = query.Count();
            var collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking();
            return new PaginatedList<T>(collection, pageIndex, pageSize, totalCount);
        }

        public virtual IEnumerable<R> StoredProcedure<R>(string storedProcedureName, IEnumerable<IDataParameter> parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                object[] objParameters = new object[parameters.Count()];
                List<string> executeParams = parameters.Select((v, i) =>
                {
                    string paramName = v.ParameterName;
                    v.ParameterName = string.Format("{0}{1}", paramName, i);
                    objParameters[i] = v;
                    return string.Format("{0} = {1}{2}", paramName, v.ParameterName, (v.Direction == ParameterDirection.Output) ? " OUTPUT" : "");
                }).ToList();
                string executeStr = string.Format("EXEC {0} {1}", storedProcedureName, string.Join(", ", executeParams));

                var retSet = Context.Database.SqlQuery<R>(executeStr, objParameters);
                return retSet.ToList();
            }
            else
            {
                var retSet = Context.Database.SqlQuery<R>(string.Format("EXEC {0}", storedProcedureName));
                return retSet.ToList();
            }
        }

        public virtual IEnumerable<T> StoredProcedureForModel(string storedProcedureName, IEnumerable<IDataParameter> parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                object[] objParameters = new object[parameters.Count()];
                List<string> executeParams = parameters.Select((v, i) =>
                {
                    string paramName = v.ParameterName;
                    v.ParameterName = string.Format("{0}{1}", paramName, i);
                    objParameters[i] = v;
                    return string.Format("{0} = {1}{2}", paramName, v.ParameterName, (v.Direction == ParameterDirection.Output) ? " OUTPUT" : "");
                }).ToList();
                string executeStr = string.Format("EXEC {0} {1}", storedProcedureName, string.Join(", ", executeParams));
                var retSet = Context.Set<T>().SqlQuery(executeStr, objParameters);

                return retSet.ToList();
            }
            else
            {
                var retSet = Context.Set<T>().SqlQuery(string.Format("EXEC {0}", storedProcedureName));
                return retSet.ToList();
            }
        }
    }
}