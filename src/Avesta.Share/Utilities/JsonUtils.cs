using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class JsonUtils
    {
        public static T ConvertFromAppSetting<T>(string key)
        {

#if DEBUG
            var json = File.ReadAllText(@"appsettings.Development.json");
#else
            var json = File.ReadAllText("appsettings.json");
#endif
            var jObject = JObject.Parse(json);
            var data = jObject["Domains"];
            var result = JsonConvert.DeserializeObject<T>(data.ToString());

            return result;
        }
    }
}
