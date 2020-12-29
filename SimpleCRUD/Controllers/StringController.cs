using Shared;
using System.Web.Http;

namespace SimpleCRUD.Controllers
{
    [RoutePrefix("api/String")]
    public class StringController : ApiController
    {
        /*  jquery sample
         * 
         *  $.get('https://localhost/api/String/Encrypt', { str: 'TEST' })
         *  .done(function (r) {
         *      console.log(r);
         *  })
         *  .fail(function (r) {
         *      console.log(r);
         *  });
         */
        [Route("Encrypt")]
        public DTO.ReqResult.Result<string> GetEncrypt([FromUri] string str = "")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new DTO.ReqResult.Result<string>() { Code = CommonCode.ParameterError.ToResCode(), Message = "str不能為空" };
            }

            return new DTO.ReqResult.Result<string>()
            {
                Data = str.Encrypt(),
                Code = CommonCode.OK.ToResCode(),
                Message = CommonCode.OK.ToStringValue()
            };
        }

        [Route("Decrypt")]
        public DTO.ReqResult.Result<string> GetDecrypt([FromUri] string str = "")
        {
            var result = new DTO.ReqResult.Result<string>(CommonCode.ParameterError);
            if (string.IsNullOrWhiteSpace(str))
            {
                result.Message = "str不能為空";
            }
            else
            {
                try
                {
                    result.Data = str.Decrypt();
                    result.Code = CommonCode.OK.ToResCode();
                    result.Message = CommonCode.OK.ToStringValue();
                }
                catch
                {
                    result.Code = CommonCode.Fail.ToResCode();
                    result.Message = "解密失敗";
                }
            }
            return result;
        }
    }
}
