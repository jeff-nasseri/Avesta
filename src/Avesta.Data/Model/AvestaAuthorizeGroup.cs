using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaAuthorizeGroup : BaseEntity
    {
        public virtual string GroupName { get; set; }

        public virtual string? FeaturesStr { get; set; }

        [NotMapped]
        public virtual List<int>? Features
        {
            get
            {
                if (string.IsNullOrEmpty(FeaturesStr))
                    return new List<int>();
                var result = JsonConvert.DeserializeObject<List<int>>(FeaturesStr);
                return result;
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                FeaturesStr = json;
            }
        }

        public virtual ICollection<AvestaUserAuthorizeGroup> UserAuthorizeGroups { get; set; }
    }

}
