using System;
using System.Reflection;
using Shared;

namespace Shared
{
    /// <summary>
    /// System.Enum 擴充方法
    /// </summary>
    public static partial class ExtFunction
    {
        /// <summary>
        /// Reture Code To Resources Key
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToResCode(this CommonCode code)
        {
            return ((int)code).ToString();
        }

      

        /// <summary>
        /// 轉換為 StringValueAttribute 字串
        /// </summary>
        /// <param name="value">System.Enum</param>
        /// <returns>StringValueAttribute 字串</returns>
        /// <remarks>
        ///     沒有定義 StringValueAttribute 就會呼叫 Enum.ToString()<br/>
        ///     如果 StringValueAttribute 有定義多個就會取得第一個 StringValueAttribute
        /// </remarks>
        public static string ToStringValue(this System.Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return string.IsNullOrWhiteSpace(output) ? value.ToString() : output;
        }
    }
}
