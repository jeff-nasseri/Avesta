using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Avesta.Data.Entity.Context
{


    public class AvestaDbContext : DbContext
    {
        public AvestaDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public AvestaDbContext() : base()
        {
        }

    }



}
