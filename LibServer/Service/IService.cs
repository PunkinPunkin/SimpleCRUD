using LibServer.DataBase;
using log4net;
using System;
using System.Resources;

namespace LibServer.Service
{
    public interface IService : IDisposable
    {
        /// <summary>
        /// 撰寫 Log 物件
        /// </summary>
        ILog Logger { get; set; }

        /// <summary>
        /// 環境變數(Develop、Test、Production), 由 Web.Config appSettings給定
        /// </summary>
        string Environment { set; get; }

        /// <summary>
        /// 服務方法的編號
        /// </summary>
        int? Seq { get; set; }

        /// <summary>
        /// 之前服務方法的編號
        /// </summary>
        int? PreSeq { get; set; }

        /// <summary>
        /// DataBase 連線管理物件
        /// </summary>
        IUnitOfWork UnitOfWork
        {
            set;
        }
    }
}
