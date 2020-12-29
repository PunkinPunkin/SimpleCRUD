using System;
using System.Collections.Generic;

namespace Shared.DTO
{
    /// <summary>
    /// 回傳訊息
    /// </summary>
    [Serializable]
    public class RetCode
    {
        public string Environment { get; set; }
        public string AppName { get; set; }
        public string FunName { get; set; }
        public string ReturnCode { get; set; }
        public string MessageText { get; set; }

        private List<Message> _msgSequence;
        public List<Message> MsgSequence
        {
            get
            {
                if (_msgSequence == null)
                    _msgSequence = new List<Message>();

                return _msgSequence;
            }
            set
            {
                _msgSequence = value;
            }
        }

        public bool IsOK
        {
            get
            {
                if (int.TryParse(ReturnCode, out int retCode))
                    return retCode == (int)CommonCode.OK;
                else
                    return false;
            }
        }

        public bool IsNoData
        {
            get
            {
                if (int.TryParse(ReturnCode, out int retCode))
                    return retCode == (int)CommonCode.NoData;
                else
                    return false;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", this.ReturnCode, this.MessageText);
        }
    }
}
