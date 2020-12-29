using Shared;

namespace DTO.ReqResult
{
    /// <summary>
    /// 通用回傳物件，只回傳二個結果資料
    /// </summary>
    /// <typeparam name="T1">回傳結果資料(1)</typeparam>
    /// <typeparam name="T2">回傳結果資料(2)</typeparam>
    public class GenTwoReqResult<T1, T2> : BaseReqResult
    {
        /// <summary>
        /// 結果資料(1)
        /// </summary>
        public T1 Result_01 { get; set; }

        /// <summary>
        /// 結果資料(2)
        /// </summary>
        public T2 Result_02 { get; set; }
    }
}
