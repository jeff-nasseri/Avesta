using Avesta.Data.Context;
using AvestaWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AvestaWebAPI.Data
{
    public class AppDbContext : AvestaDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AvestaCrudEntity> Model { get; set; }

    }


}
