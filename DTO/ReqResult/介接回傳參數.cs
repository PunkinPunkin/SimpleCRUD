using Shared;
using Shared.DTO;
using System.Collections.Generic;

namespace DTO.ReqResult
{
    public interface IBasicResult
    {
        string Code { get; set; }
        string Message { get; set; }
    }

    public class BasicResult : IBasicResult
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public BasicResult()
        {
            Code = CommonCode.Fail.ToResCode();
            Message = "伺服器錯誤，請聯絡相關人員。";
        }

        public BasicResult(CommonCode commonCode)
        {
            Code = commonCode.ToResCode();
            Message = commonCode.ToStringValue();
        }

        public BasicResult(RetCode retCode)
        {
            Code = retCode.ReturnCode;
            Message = retCode.MessageText;
        }
    }

    /// <summary>
    /// 回傳基本格式
    /// </summary>
    /// <typeparam name="T">回傳結果資料(1)</typeparam>
    public class Result<T> : BasicResult
    {
        public T Data { get; set; }

        public Result() : base() { }
        public Result(CommonCode commonCode) : base(commonCode) { }
        public Result(RetCode retCode) : base(retCode) { }
    }

    /// <summary>
    /// 回傳基本格式
    /// </summary>
    /// <typeparam name="T">回傳結果資料(1)</typeparam>
    public class ResultList<T> : BasicResult
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public ResultList() : base() { }
        public ResultList(CommonCode commonCode) : base(commonCode) { }
        public ResultList(RetCode retCode) : base(retCode) { }
    }

    /// <summary>
    /// 回傳Token格式
    /// </summary>
    public class TokenResut : BasicResult
    {
        public string Token { get; set; }
    }
}