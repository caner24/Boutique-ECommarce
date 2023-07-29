using Boutique.Core.DataAccess;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Abstract
{
    public interface IPhotosService:IQueryableRepository<Photos>
    {
        public Task<Photos> Get(Expression<Func<Photos, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<Photos>> GetAll(
         Expression<Func<Photos, bool>> filter = null,
  Func<IQueryable<Photos>, IOrderedQueryable<Photos>> orderBy = null,
         string includeProperties = "");
        public Photos Update(Photos entity);
        public Task<Photos> Add(Photos entity);
        public int Delete(Photos entity);
    }
}
