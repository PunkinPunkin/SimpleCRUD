using System.Runtime.Serialization;

namespace Shared
{
    /// <summary>
    /// 訊息型態
    /// </summary>
    [DataContract]
    public enum MsgType
    {
        /// <summary>
        /// Debug
        /// </summary>
        [EnumMember]
        DEBUG,
        /// <summary>
        /// Info
        /// </summary>
        [EnumMember]
        INFO,
        /// <summary>
        /// Warning
        /// </summary>
        [EnumMember]
        WARN,
        /// <summary>
        /// Error
        /// </summary>
        [EnumMember]
        ERROR,
        /// <summary>
        /// Fatal
        /// </summary>
        [EnumMember]
        FATAL,
        /// <summary>
        /// MsgCode
        /// </summary>
        [EnumMember]
        MSGCODE,
    }
}
