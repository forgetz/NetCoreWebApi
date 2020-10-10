using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ResponseResult
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public ResponseResult(int code, string status, string message = "", object result = null)
        {
            Code = code;
            Status = status;
            Message = message;
            Result = result;
        }

    }


    public enum StatusMessage
    {
        Completed,
        Error,
        NotFound
    }

}
