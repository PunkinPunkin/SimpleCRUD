namespace DTO.ReqInParm
{
    /// <summary>
    /// 通用請求物件，一個請求參數
    /// </summary>
    /// <typeparam name="T1">請求參數(1)</typeparam>
    public class GenOneReqInParm<T1>
    {
        /// <summary>
        /// 請求參數(1)
        /// </summary>
        public T1 Parm_01 { get; set; }
    }
}
