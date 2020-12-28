using System.Runtime.Serialization;
namespace Shared
{
    /// <summary>
    /// 環境型態
    /// </summary>
    [DataContract]
    public enum EnvType
    {
        /// <summary>
        /// 開發環境
        /// </summary>
        [EnumMember]
        Develop,

        /// <summary>
        /// 客戶測試環境
        /// </summary>
        [EnumMember]
        Test,      

        /// <summary>
        /// 正式環境
        /// </summary>
        [EnumMember]
        Production,

        /// <summary>
        /// 公司內環境
        /// </summary>
        [EnumMember]
        Internal,

    }
}
