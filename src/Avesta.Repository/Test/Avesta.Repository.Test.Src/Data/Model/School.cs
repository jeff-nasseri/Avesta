using Avesta.Data.Entity.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Data.Model
{
    public class School : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Student> Students { get; set; }

        [JsonIgnore]
        public ICollection<Teacher_School> Teacher_Schools { get; set; }

    }
}
