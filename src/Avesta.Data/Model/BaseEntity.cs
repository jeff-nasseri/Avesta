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
        public BaseEntity() : base()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Counter { get; set; }
    }


    public abstract class BaseEntity<T> where T : class
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T ID { get; set; }


        public virtual bool IsLock { get; set; } = false;


        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
    }
}
