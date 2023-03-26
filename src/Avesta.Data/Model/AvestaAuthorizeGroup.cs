using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaAuthorizeGroup<TId, TAvestaUserGroup> : BaseEntity<TId>
        where TId : class
        where TAvestaUserGroup : AvestaUserAuthorizeGroup<TId>
    {
        public virtual string GroupName { get; set; }

        public virtual string? AccessStr { get; set; }

        [NotMapped]
        public virtual List<string>? Access
        {
            get
            {
                if (string.IsNullOrEmpty(AccessStr))
                    return new List<string>();
                var result = JsonConvert.DeserializeObject<List<string>>(AccessStr);
                return result;
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                AccessStr = json;
            }
        }

        public virtual ICollection<TAvestaUserGroup>? UserAuthorizeGroups { get; set; }
    }


    public class AvestaAuthorizeGroup<TAvestaUserGroup> : AvestaAuthorizeGroup<string, TAvestaUserGroup>
        where TAvestaUserGroup : AvestaUserAuthorizeGroup<string>
    {
    }

}
