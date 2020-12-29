using Shared;

namespace DTO.ReqResult
{
    /// <summary>
    /// 通用回傳物件，只回傳單一字串
    /// </summary>
    public class GenOneStringReqResult : BaseReqResult
    {
        /// <summary>
        /// 結果字串
        /// </summary>
        public string Text { get; set; }
    }
}