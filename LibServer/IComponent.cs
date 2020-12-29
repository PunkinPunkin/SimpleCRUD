using LibServer.DataBase;
using log4net;
using System.Resources;

namespace LibServer
{
    public interface IComponent
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
        /// DataBase 連線管理物件
        /// </summary>
        IUnitOfWork UnitOfWork { set; }
    }
}
