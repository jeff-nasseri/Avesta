using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.API
{
    public class ResponseModel : BaseModel
    {
        public enum Status : int
        {
            Success = 1200,
            Fail = 1500
        }


        public ResponseModel(Status status, object data, string message, bool successfull)
        {
            StatusNumber = status;
            Data = data;
            Message = message;
            Successfull = successfull;
        }

        public ResponseModel()
        {
        }

        public ResponseModel Success(object data, string message = null)
            => Init(Status.Success, data, message ?? "Success", true);

        public ResponseModel Fail(object data, string message = null)
            => Init(Status.Fail, data, message ?? "Fail", false);





        public ResponseModel Init(Status status, object data, string message, bool successfull)
        {
            Data = data;
            StatusNumber = status;
            Message = message;
            Successfull = successfull;

            return this;
        }


        public Status StatusNumber { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Successfull { get; set; }
    }
}
