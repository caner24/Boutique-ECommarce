using Boutique.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Boutique.Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TContext, TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        public async Task<TEntity> Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }

        }
        public int Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                return context.SaveChanges();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAll(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "", int skip = 0, int take = 0)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (includeProperties != "")
                {
                    foreach (var includeProperty in includeProperties.Split
                                 (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                if (orderBy != null)
                {
                    return skip != 0 ? await orderBy(query).Skip(skip).Take(take).AsNoTracking().ToListAsync() : await orderBy(query).Take(take).AsNoTracking().ToListAsync();
                }
                else
                {

                    return await query.ToListAsync();
                }
            }
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (includeProperties != "")
                {
                    foreach (var includeProperty in includeProperties.Split
                               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.AsNoTracking().FirstOrDefaultAsync(filter);
            }
        }
        public TEntity Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().Update(entity);
                context.SaveChanges();
                return entity;
            }
        }
    }
}
