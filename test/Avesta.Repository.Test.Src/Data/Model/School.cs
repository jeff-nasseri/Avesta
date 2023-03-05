using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Data.Model
{
    public class School : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Teacher_School> Teacher_Schools { get; set; }

    }
}
