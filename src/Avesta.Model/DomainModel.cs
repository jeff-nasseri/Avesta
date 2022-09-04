using Avesta.Share.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public class Domain
    {
        public static string CDNDomain = AppsDomain.Domains.First(d => d.Domain == WitchDomain.CDN).URL;
    }

    public class AppsDomain
    {
        public const string Key = "Domains";

        public static List<AppDomain> Domains = JsonUtils.ConvertFromAppSetting<List<AppDomain>>(Key);
    }
    public class AppDomain
    {
        public WitchDomain Domain { get; set; }
        public string URL { get; set; }
    }

    public enum WitchDomain
    {
        CDN,
        Dashboard,
        Site,
        Support,
        Payment
    }

}
