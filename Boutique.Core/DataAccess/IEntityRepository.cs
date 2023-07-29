using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;


namespace Boutique.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<TEntity>> GetAll(
              Expression<Func<TEntity, bool>> filter = null,
              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
              string includeProperties = "",int skip = 0, int take = 0);

        public TEntity Update(TEntity entity);
        public Task<TEntity> Add(TEntity entity);
        public int Delete(TEntity entity);
    }
}
