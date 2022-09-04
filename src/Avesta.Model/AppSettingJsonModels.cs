using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{

    public enum DataBaseNames
    {
        MSSQL,
        SQLITE
    }
    public enum OSNames
    {
        Linux,
        Windows
    }
    public class PlatformSettingModel
    {
        public const string Key = "Platform";

        public OSNames OS { get; set; }
        public DataBaseNames DataBase { get; set; }
        public string ConnectionString { get; set; }

    }
}
