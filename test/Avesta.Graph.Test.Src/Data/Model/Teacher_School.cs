using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Test.Src.Data.Model
{
    public class Teacher_School : BaseEntity
    {
        [ForeignKey(nameof(Teacher))]
        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }


        [ForeignKey(nameof(School))]
        public string? SchoolId { get; set; }
        public School? School { get; set; }
    }

}
