using Avesta.Data.Entity.Model;
using Avesta.Exceptions.Entity;
using Avesta.Exceptions.Reflection;
using Avesta.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Avesta.Share.Model;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Hosting;
using MoreLinq;
using MoreLinq.Extensions;

using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Delete;
using System.Threading;
using Avesta.Constant;
using Avesta.Share.Utilities;
using Avesta.Data.Entity.Context;

namespace Avesta.Repository.Entity
{

    public abstract class BaseRepository<TContext> : IDisposable
        where TContext : AvestaDbContext
    {

        readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }


        public virtual DbSet<TEntity> Table<TEntity, TId>()
            where TId : class
            where TEntity : BaseEntity<TId>
                => _context.Set<TEntity>();

        public virtual IQueryable<TEntity> Query<TEntity, TId>()
            where TId : class
            where TEntity : BaseEntity<TId>
                => _context.Set<TEntity>().AsQueryable();



        public virtual async Task SaveChanges(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<int> SaveChanges(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
               => await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);


        public virtual IQueryable<TEntity> IncludeByPath<TEntity, TId>(string navigationPropertyPath)
        where TId : class
        where TEntity : BaseEntity<TId>
        {
            var table = _context.Set<TEntity>().AsQueryable();

            if (string.IsNullOrEmpty(navigationPropertyPath))
                return table;

            var props = navigationPropertyPath.Split(";");

            foreach (var prop in props)
            {
                table = table.Include(prop);
            }
            return table;
        }

        public virtual IQueryable<TEntity> IncludeAll<TEntity, TId>()
         where TId : class
         where TEntity : BaseEntity<TId>
        {
            var table = _context.Set<TEntity>().AsQueryable();

            var navigations = _context.Model.FindEntityType(typeof(TEntity))
                .GetDerivedTypesInclusive()
                .SelectMany(type => type.GetNavigations())
                .Distinct();

            foreach (var property in navigations)
                table = table.Include(property.Name);

            return table;
        }




        public void ChangeState<TEntity, TId>(TEntity entity, EntityState state)
            where TId : class
            where TEntity : BaseEntity<TId>
                => _context.Entry(entity).State = state;






        bool disposed = false;
        public void Dispose()
        {
            if (!disposed)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
                disposed = !disposed;
            }
        }



    }









    




}
