using System;
using System.Linq;
using System.Text;

namespace Shared
{
    /// <summary>
    /// System.Text.StringBuilder 擴充方法
    /// </summary>
    public static partial class ExtFunction
    {
        /// <summary>
        /// 將指定字串移除所有的前置和後端空白字元的複本附加至這個執行個體。
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="value">要附加的字串。</param>
        /// <returns>完成附加作業之後，這個執行個體的參考。</returns>
        public static StringBuilder AppendTrim(this StringBuilder sb, string value)
        {            
            return sb.Append(string.Format(" {0} ", value.Trim())); ;
        }

        /// <summary>
        /// 將指定字串移除所有的前置和後端空白字元的複本附加至這個執行個體。
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="value">要附加的字串。</param>
        /// <param name="arg0">要格式化的物件。</param>
        /// <returns>完成附加作業之後，這個執行個體的參考。</returns>
        public static StringBuilder AppendTrimFormat(this StringBuilder sb, string value, object arg0)
        {
            return sb.AppendFormat(string.Format(" {0} ", value.Trim()),arg0);
        }

        /// <summary>
        /// 將指定字串移除所有的前置和後端空白字元的複本附加至這個執行個體。
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="value">要附加的字串。</param>
        /// <param name="arg0">要格式化的第一個物件。</param>
        /// <param name="arg1">要格式化的第二個物件。</param>
        /// <returns>完成附加作業之後，這個執行個體的參考。</returns>
        public static StringBuilder AppendTrimFormat(this StringBuilder sb, string value, object arg0, object arg1)
        {
            return sb.AppendFormat(string.Format(" {0} ", value.Trim()), arg0, arg1);
        }

        /// <summary>
        /// 將指定字串移除所有的前置和後端空白字元的複本附加至這個執行個體。
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="value">要附加的字串。</param>
        /// <param name="arg0">要格式化的第一個物件。</param>
        /// <param name="arg1">要格式化的第二個物件。</param>
        /// <param name="arg2">要格式化的第三個物件。</param>
        /// <returns>完成附加作業之後，這個執行個體的參考。</returns>
        public static StringBuilder AppendTrimFormat(this StringBuilder sb, string value, object arg0, object arg1, object arg2)
        {
            return sb.AppendFormat(string.Format(" {0} ", value.Trim()), arg0, arg1, arg2);
        }

        /// <summary>
        /// 將指定字串移除所有的前置和後端空白字元的複本附加至這個執行個體。
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="value">要附加的字串。</param>
        /// <param name="args">要格式化的物件陣列。</param>
        /// <returns>完成附加作業之後，這個執行個體的參考。</returns>
        public static StringBuilder AppendTrimFormat(this StringBuilder sb, string value, params object[] args)
        {
            return sb.AppendFormat(string.Format(" {0} ", value.Trim()), args);
        }

        /// <summary>
        /// 傳回字串，這個字串包含某個字串中從指定之位置開始的所有字元。
        /// </summary>
        /// <param name="str">字元會從此運算式中傳回。</param>
        /// <param name="start">要傳回字元的開始位置。如果 Start 大於 str 中的字元數，則 Mid 函式會傳回長度為零的字串 ("")； Start 是以一起始。</param>
        /// <returns>含字串中從指定之位置開始的指定數目字元。</returns>
        public static string Mid(this string str, int start)
        {
            string result = string.Empty;
            if (str.Length > start)
            {
                result = str.Substring(start - 1);
            }
            return result;
        }

        /// <summary>
        /// 傳回字串，這個字串包含某個字串中從指定之位置開始的指定數目字元。
        /// </summary>
        /// <param name="str">字元會從此運算式中傳回。</param>
        /// <param name="start">要傳回字元的開始位置。如果 Start 大於 str 中的字元數，則 Mid 函式會傳回長度為零的字串 ("")； Start 是以一起始。</param>
        /// <param name="length">要傳回的字元數。如果省略，或者此字元數少於文字中的 Length 字元 (包括 Start 位置上的字元)，則會傳回從起始位置到字串結尾的所有字元。</param>
        /// <returns>含字串中從指定之位置開始的指定數目字元。</returns>
        public static string Mid(this string str, int start, int length)
        {
            string result = string.Empty;
            string temp = Mid(str, start);
            if (!string.IsNullOrEmpty(temp))
            {
                if (temp.Length < length)
                {
                    length = temp.Length;
                }
                result = temp.Substring(0, length);
            }
            return result;
        }

        /// <summary>
        /// 找字串中指定位置之後不同字元的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int NextDiffChar(this string str, int index)
        {
            char nextChar = str.Substring(index).FirstOrDefault(r => r != str[index]);
            if (nextChar != '\0')
            {
                return index + str.Substring(index).IndexOf(nextChar);
            }
            else
            {
                return str.Length;
            }
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(this string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
    }
}
