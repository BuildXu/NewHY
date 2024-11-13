using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class ResultData
    {
        public Result Result { get; set; }
    }
    // Result类，对应JSON中的 "Result" 部分
    public class Result
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string Id { get; set; }
        public string Number { get; set; }
        public List<NeedReturnData> NeedReturnData { get; set; }
    }

    // ResponseStatus类，对应JSON中的 "ResponseStatus" 部分
    public class ResponseStatus
    {
        public int ErrorCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<Error> Errors { get; set; }
        public List<object> SuccessEntitys { get; set; }
        public List<string> SuccessMessages { get; set; }
        public int MsgCode { get; set; }
    }

    // Error类，对应JSON中的 "Errors" 列表中的每个元素
    public class Error
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public int DIndex { get; set; }
    }

    // NeedReturnData类，对应JSON中的 "NeedReturnData" 列表中的每个元素
    public class NeedReturnData
    {
        public int FID { get; set; }
        public string FBillNo { get; set; }
    }
}