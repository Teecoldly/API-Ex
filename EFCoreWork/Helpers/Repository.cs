using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCoreWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreWork.Helpers
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        public DbContext _context;
        public IServiceProvider _serviceProvider;


        private IQueryable<TEntity> Querydb(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query.Where(filter);
            }
            return query;

        }

        public Repository(IServiceProvider serviceProvider, DbContext Context)
        {
            _context = Context;

            _serviceProvider = serviceProvider;
        }


        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return Querydb(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Querydb(null).ToList();
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null)
        {
            return Querydb(filter).ToList();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
