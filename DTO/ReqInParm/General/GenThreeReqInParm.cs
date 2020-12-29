namespace DTO.ReqInParm
{
    /// <summary>
    /// 通用請求物件，三個請求參數
    /// </summary>
    /// <typeparam name="T1">請求參數(1)</typeparam>
    /// <typeparam name="T2">請求參數(2)</typeparam>
    /// <typeparam name="T3">請求參數(3)</typeparam>
    public class GenThreeReqInParm<T1, T2, T3> 
    {
        /// <summary>
        /// 請求參數(1)
        /// </summary>
        public T1 Parm_01 { get; set; }

        /// <summary>
        /// 請求參數(2)
        /// </summary>
        public T2 Parm_02 { get; set; }

        /// <summary>
        /// 請求參數(3)
        /// </summary>
        public T3 Parm_03 { get; set; }
    }
}