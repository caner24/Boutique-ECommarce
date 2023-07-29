using Boutique.Business.Abstract;
using Boutique.DataAccess.Abstract;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class VisitManager : IVisitService
    {
        private readonly IVisitDal _visitManager;
        public VisitManager(IVisitDal visitManager)
        {
            _visitManager = visitManager;
        }
        public async Task<Visits> Add(Visits entity)
        {
          return await _visitManager.Add(entity);
        }

        public int Delete(Visits entity)
        {
            throw new NotImplementedException();
        }

        public Task<Visits> Get(Expression<Func<Visits, bool>> filter, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Visits>> GetAll(Expression<Func<Visits, bool>> filter = null, Func<IQueryable<Visits>, IOrderedQueryable<Visits>> orderBy = null, string includeProperties = "")
        {
            return await  _visitManager.GetAll(filter,orderBy,includeProperties);
        }

        public Visits Update(Visits entity)
        {
            throw new NotImplementedException();
        }
    }
}
