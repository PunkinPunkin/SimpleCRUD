using Shared;

namespace DTO.ReqResult
{
    /// <summary>
    /// 通用回傳物件，只回傳一個結果資料
    /// </summary>
    /// <typeparam name="T1">回傳結果資料(1)</typeparam>
    public class GenOneReqResult<T1> : BaseReqResult
    {
        /// <summary>
        /// 結果資料(1)
        /// </summary>
        public T1 Result_01 { get; set; }
    }
}
