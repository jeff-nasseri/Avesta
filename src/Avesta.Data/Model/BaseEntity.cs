using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class BaseEntity : BaseEntity<string>
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Counter { get; set; }
    }


    public class BaseEntity<T> where T : class
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            ModifiedDate = CreateDate;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T ID { get; set; }


        public bool IsLock { get; set; } = false;


        public DateTime? ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
