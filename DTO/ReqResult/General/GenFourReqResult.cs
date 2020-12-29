using Shared;

namespace DTO.ReqResult
{
    /// <summary>
    /// 通用回傳物件，只回傳四個結果資料
    /// </summary>
    /// <typeparam name="T1">回傳結果資料(1)</typeparam>
    /// <typeparam name="T2">回傳結果資料(2)</typeparam>
    /// <typeparam name="T3">回傳結果資料(3)</typeparam>
    /// <typeparam name="T4">回傳結果資料(4)</typeparam>
    public class GenFourReqResult<T1, T2, T3, T4> : BaseReqResult
    {
        /// <summary>
        /// 結果資料(1)
        /// </summary>
        public T1 Result_01 { get; set; }

        /// <summary>
        /// 結果資料(2)
        /// </summary>
        public T2 Result_02 { get; set; }

        /// <summary>
        /// 結果資料(3)
        /// </summary>
        public T3 Result_03 { get; set; }

        /// <summary>
        /// 結果資料(4)
        /// </summary>
        public T4 Result_04 { get; set; }
    }
}