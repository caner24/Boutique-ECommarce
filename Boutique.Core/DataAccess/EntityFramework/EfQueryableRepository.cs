using Boutique.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Core.DataAccess.EntityFramework
{
    public class EfQueryableRepository<T,TContext> : IQueryableRepository<T> where T : class, IEntity, new() where TContext : DbContext,new()
    {
        private DbContext? _context;
        private DbSet<T>? _entities;
        public EfQueryableRepository()
        {
            _context = new TContext();
        }

        public IQueryable<T> Table => this.Entities;

        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
