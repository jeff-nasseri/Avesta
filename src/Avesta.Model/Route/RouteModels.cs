using Avesta.Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Route
{
    public class EndPointModel
    {
        public string Root { get; set; }
        public string RootName { get; set; }
        public bool ShowAble { get; set; } = false;
        public string FeaturesNeedAuthorizedAccess { get; set; }
    }



    public class PageRouteModel
    {
        public EndPointModel Controller { get; set; }
        public IEnumerable<EndPointModel> Actions { get; set; }
    }


  
}
