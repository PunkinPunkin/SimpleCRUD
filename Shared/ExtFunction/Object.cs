using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shared
{
    /// <summary>
    /// Object 的擴充方法
    /// </summary>
    public static partial class ExtFunction
    {
        /// <summary>
        /// 深層clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(this object obj) where T : class
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return formatter.Deserialize(stream) as T;
        }
    } 
}
