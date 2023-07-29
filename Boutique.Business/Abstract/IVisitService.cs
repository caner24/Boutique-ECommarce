using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Abstract
{
    public interface IVisitService
    {
        public Task<Visits> Get(Expression<Func<Visits, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<Visits>> GetAll(
         Expression<Func<Visits, bool>> filter = null,
  Func<IQueryable<Visits>, IOrderedQueryable<Visits>> orderBy = null,
         string includeProperties = "");
        public Visits Update(Visits entity);
        public Task<Visits> Add(Visits entity);
        public int Delete(Visits entity);
    }
}
