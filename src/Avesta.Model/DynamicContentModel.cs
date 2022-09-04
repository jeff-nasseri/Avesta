using Avesta.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public enum DynamicContent
    {
        SiteName = 1,
        SiteSlogan = 2
    }
    public class DynamicContentModel 
    {
        public string ContentName { get; set; }
        public DynamicContent DynamicContent { get; set; }
    }
    public class DynamicContentStorage
    {
        const string Prefix = "dynamic.content.";

        static string GetRootNameByCode(int code)
        {
            var key = $"{DynamicContentStorage.Prefix}{code}";
            return Lang.T(key);
        }

        public static List<DynamicContentModel> Models = new()
        {
            new DynamicContentModel
            {
                DynamicContent = DynamicContent.SiteName,
                ContentName = GetRootNameByCode((int)DynamicContent.SiteName)
            },
            new DynamicContentModel
            {
                DynamicContent = DynamicContent.SiteSlogan,
                ContentName = GetRootNameByCode((int)DynamicContent.SiteSlogan)
            }
        };
    }
}
