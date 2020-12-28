using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Shared
{
    public static partial class ExtFunction
    {
        public static string ToString(this SecureString value, int length)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                StringBuilder sb = new StringBuilder();
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                //Fortify顧問建議採取拼接的方式或者一些不是能一次性看出該值到底是什麽内容的間接方式，取代 Marshal.PtrToStringUni(valuePtr);
                for (int i = 0; i < length; i++)
                {
                    // multiply 2 because Unicode chars are 2 bytes wide
                    sb.Append((char)Marshal.ReadInt16(valuePtr, i * 2));
                }
                return sb.ToString();
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
