using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public class ActionInformation : BaseModel
    {
        public DateTime OccureTime { get; private set; }
        public string Comment { get; private set; }


        public ActionInformation()
        {
        }
        public ActionInformation(string comment)
        {
            Comment = comment;
        }

        public ActionInformation StartAt(string comment)
        {
            this.Comment = comment;
            this.OccureTime = DateTime.UtcNow;
            return this;
        }

        public ActionInformation StartAt()
        {
            this.OccureTime = DateTime.UtcNow;
            return this;
        }

    }

}
