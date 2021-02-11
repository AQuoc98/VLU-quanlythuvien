using CoreApp.Common.Enums;
using System;

namespace CoreApp.Common.Models
{
    public class ResultModel<T>
    {
        public Guid RecordId { get; set; }
        public ResultStatus Status { get; set; }
        public string[] Messages { get; set; }
        public T ExtendData { get; set; }
      
        public ResultModel(Guid recordId, ResultStatus status, string[] message = null, dynamic extendData = null)
        {
            ExtendData = extendData;
            RecordId = recordId;
            Status = status;
            if (message != null)
                Messages = message;
        }

        public ResultModel()
        {

        }
        // public static ResultModel<T> GetResult (int code , string message, dynamic extendData = null)
        //{
        //    return new ResultModel<T> {
        //        Code = code,
        //        ExtendData = extendData,
        //        Mgs = message
        //    };
           
        
        //}
     
    }

    public class ResultModel : ResultModel<object>
    {
        public ResultModel(Guid recordId, ResultStatus status, string[] message = null, dynamic extendData = null)
        {
            ExtendData = extendData;
            RecordId = recordId;
            Status = status;
            if (message != null)
                Messages = message;
        }

        public ResultModel()
        {

        }
    }
}
