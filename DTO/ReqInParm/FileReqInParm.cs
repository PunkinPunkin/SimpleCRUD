using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DTO.ReqInParm
{
    public interface IFileReqInParm
    {
        /// <summary>
        /// 檔案名稱
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 檔案名稱(含當下時間)
        /// </summary>
        string NameWithTimespan { get; }
        /// <summary>
        /// 副檔名
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// 檔案內容
        /// </summary>
        byte[] Content { get; set; }
    }

    public class FileReqInParm : IFileReqInParm
    {
        protected string name;
        protected string nameWithTimespan;
        protected readonly string createTime;
        public FileReqInParm() { createTime = DateTime.Now.ToString("yyyyMMddHHmmssffff"); }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        [Required]
        [DisplayName("上傳檔案名稱")]
        [MaxLength(255, ErrorMessage = "{0} 最大長度為{1}。")]
        [RegularExpression(@"[^\\]*\.[A-Za-z]{3,4}$", ErrorMessage = "{0}格式錯誤。")]
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                nameWithTimespan = string.Empty;
                name = value;
            }
        }

        /// <summary>
        /// 檔案名稱(含當下時間)
        /// </summary>
        [JsonIgnore]
        [DisplayName("檔案名稱")]
        public virtual string NameWithTimespan
        {
            get
            {
                if (string.IsNullOrEmpty(name) || name.IndexOf(".") < 0) return string.Empty;

                if (string.IsNullOrEmpty(nameWithTimespan))
                {
                    nameWithTimespan = name.Substring(0, name.Length - name.Split('.').Last().Length - 1) + "_" + createTime + Extension;
                }
                return nameWithTimespan;
            }
        }

        /// <summary>
        /// 副檔名
        /// </summary>
        [JsonIgnore]
        [DisplayName("副檔名")]
        public virtual string Extension
        {
            get
            {
                if (!string.IsNullOrEmpty(name) && name.IndexOf(".") > 0)
                {
                    return "." + name.Split('.').Last();
                }
                return string.Empty;
            }
        }

        [Required]
        [DisplayName("檔案")]
        public byte[] Content { get; set; }
    }

    public class ImageReqInParm : FileReqInParm, IFileReqInParm
    {
        public ImageReqInParm() : base() { }

        [Required]
        [DisplayName("上傳檔案名稱")]
        [MaxLength(255, ErrorMessage = "{0} 最大長度為{1}。")]
        [RegularExpression(@"^.[^\\\/\:\*\?\""<>\|]*\.(bmp|gif|jpg|jpeg|png|tif|tiff)$", ErrorMessage = "{0}格式錯誤。")]
        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                nameWithTimespan = string.Empty;
                name = value;
            }
        }
    }
}
