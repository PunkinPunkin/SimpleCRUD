using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace LibServer.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="entity">要新增到的Entity</param>
        void Create(T entity);

        /// <summary>
        /// 更新一筆資料的內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        void Update(T entity);

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        void Delete(T entity);

        /// <summary>
        /// 儲存異動。
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 用主鍵查詢。
        /// </summary>
        /// <param name="keys">Key</param>
        /// <returns>取得符合條件的內容。</returns>
        T Get(object keys);

        /// <summary>
        /// 取得全部的內容。
        /// </summary>
        IQueryable<T> GetAll();

        PaginatedList<T> GetPaged(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, IList<Expression<Func<T, object>>> includedProperties = null);

        /// <summary>
        /// 取得符合條件的內容。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得符合條件的內容。</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 執行預存程序
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="storedProcedureName">預存程序名稱</param>
        /// <param name="parameters">傳入的參數</param>
        /// <returns>預存程序返回的值</returns>
        IEnumerable<R> StoredProcedure<R>(string storedProcedureName, IEnumerable<IDataParameter> parameters = null);

        IEnumerable<T> StoredProcedureForModel(string storedProcedureName, IEnumerable<IDataParameter> parameters = null);
    }
}
