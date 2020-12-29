using Shared;

namespace DTO.ReqResult
{
    /// <summary>
    /// 通用回傳物件，只回傳三個結果資料
    /// </summary>
    /// <typeparam name="T1">回傳結果資料 1.</typeparam>
    /// <typeparam name="T2">回傳結果資料 2.</typeparam>
    /// <typeparam name="T3">回傳結果資料 3.</typeparam>
    public class GenThreeReqResult<T1, T2, T3> : BaseReqResult
    {
        /// <summary>
        /// Gets or sets 結果資料 1.
        /// </summary>
        /// <value>
        /// 結果資料 1.
        /// </value>
        public T1 Result_01 { get; set; }

        /// <summary>
        /// Gets or sets 結果資料 2.
        /// </summary>
        /// <value>
        /// 結果資料 2.
        /// </value>
        public T2 Result_02 { get; set; }

        /// <summary>
        /// Gets or sets 結果資料 3.
        /// </summary>
        /// <value>
        /// 結果資料 3.
        /// </value>
        public T3 Result_03 { get; set; }
    }
}
