using Boutique.Business.Abstract;
using Boutique.Core.DataAccess.EntityFramework;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class PhotosManager :EfQueryableRepository<Photos,BoutiqueContext> ,IPhotosService
    {
        private readonly IPhotosDal _photosManager;

        public PhotosManager(IPhotosDal photosManager)
        {
            _photosManager = photosManager;
        }
        public async Task<Photos> Add(Photos entity)
        {
           return await _photosManager.Add(entity);
        }


        public int Delete(Photos entity)
        {
            return _photosManager.Delete(entity);
        }

        public Task<Photos> Get(Expression<Func<Photos, bool>> filter, string includeProperties = "")
        {
           return _photosManager.Get(filter, includeProperties);
        }

        public Task<IEnumerable<Photos>> GetAll(Expression<Func<Photos, bool>> filter = null, Func<IQueryable<Photos>, IOrderedQueryable<Photos>> orderBy = null, string includeProperties = "")
        {
            return _photosManager.GetAll(filter,orderBy,includeProperties);
        }

        public Photos Update(Photos entity)
        {
            return _photosManager.Update(entity);
        }

    }
}
