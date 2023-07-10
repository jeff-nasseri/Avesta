using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Avesta.Security.Hash.Extension;
using System.Security;

namespace Avesta.Data.Entity.Model
{
    public interface IBaseEntity<TId> where TId : class
    {
        TId ID { get; set; }
        bool IsLock { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime CreatedDate { get; }
        DateTime? DeletedDate { get; }
    }

    public class BaseEntity : BaseEntity<string>
    {
        public BaseEntity() : base()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Counter { get; set; }
    }


    public abstract class BaseEntity<T> : IBaseEntity<T>
        where T : class
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            Hash = this.MakeItSha256();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T ID { get; set; }


        public virtual bool IsLock { get; set; } = false;


        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime CreatedDate { get; private set; }
        public virtual DateTime? DeletedDate { get; private set; }




        public virtual void SoftDelete() => DeletedDate = DateTime.UtcNow;


        public virtual string Report()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }



        [NotMapped]
        public string Hash { get; private set; } = string.Empty;


        public virtual void Validate()
        {
            var temp = Hash;
            Hash = string.Empty;
            var hash = this.MakeItSha256();

            if (hash == temp)
                Hash = hash;
            else
                throw new SecurityException("Anti Hijack Exception Raise !");
        }


    }


}


