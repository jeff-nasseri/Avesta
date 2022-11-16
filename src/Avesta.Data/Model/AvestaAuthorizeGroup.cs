using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaAuthorizeGroup<TAvestaUserGroup> : BaseEntity
        where TAvestaUserGroup : AvestaUserAuthorizeGroup
    {
        public virtual string GroupName { get; set; }

        public virtual string? FeaturesStr { get; set; }

        [NotMapped]
        public virtual List<string>? Features
        {
            get
            {
                if (string.IsNullOrEmpty(FeaturesStr))
                    return new List<string>();
                var result = JsonConvert.DeserializeObject<List<string>>(FeaturesStr);
                return result;
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                FeaturesStr = json;
            }
        }

        public virtual ICollection<TAvestaUserGroup>? UserAuthorizeGroups { get; set; }
    }

}
