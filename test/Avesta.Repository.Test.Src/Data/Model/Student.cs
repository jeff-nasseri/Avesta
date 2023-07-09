using Avesta.Data.Entity.Model;
using Avesta.Repository.Test.Src.Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Data.Model
{
    public class Student : BaseEntity
    {
        public string? Fullname { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string? AnyExteraNote { get; set; }


        [ForeignKey(nameof(School))]
        public string? SchoolId { get; set; }
        public School? School { get; set; }
    }

}
