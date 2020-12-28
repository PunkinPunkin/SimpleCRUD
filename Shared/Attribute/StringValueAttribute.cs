using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    /// <summary>
    /// 列舉附加名稱属性
    /// </summary>
    public class StringValueAttribute : System.Attribute
    {
        private string _value;

        /// <summary>
        /// 建構元
        /// </summary>
        /// <param name="value">附加名稱</param>
        public StringValueAttribute(string value)
        {
            _value = value;
        }
        /// <summary>
        /// 附加名稱
        /// </summary>
        public string Value
        {
            get { return _value; }
        }
    }
}
