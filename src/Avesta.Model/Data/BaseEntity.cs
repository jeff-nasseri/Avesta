using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Data
{
    public class BaseEntity
    {

        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            ModifiedDate = CreateDate;
        }



        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public bool IsLock { get; set; } = false;


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Counter { get; set; }

        public DateTime ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}
