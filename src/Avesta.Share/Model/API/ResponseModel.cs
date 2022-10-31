using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.API
{
    public class ResponseModel
    {

        public ResponseModel(int status, object data, string message, bool successfull)
        {
            Status = status;
            Data = data;
            Message = message;
            Successfull = successfull;
        }

        public ResponseModel()
        {
        }
        
        public ResponseModel Success(object data)
        {
            Data = data;
            Status = 200;
            Successfull = true;

            return this;
        }



        public int Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Successfull { get; set; }
    }
}
