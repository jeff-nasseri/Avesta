using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Entity
{
    public abstract class BaseEntityService
    {
        readonly protected DateTime InitTime;

        public BaseEntityService()
        {
            InitTime = DateTime.Now;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


}
