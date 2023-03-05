using Avesta.Data.Model;
using Avesta.Repository.Test.Src.Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Data.Model
{
    public class Teacher : BaseEntity
    {
        public string? Fullname { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public ICollection<Teacher_School> Teacher_Schools { get; set; }
    }
}
