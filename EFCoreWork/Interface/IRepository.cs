using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCoreWork.Interface
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> filter = null);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);
        void AddRange(List<TEntity> entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entity);

    }
}
