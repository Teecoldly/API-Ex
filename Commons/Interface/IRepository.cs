using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Commons.Extention;
using Microsoft.EntityFrameworkCore;
using Commons.Profile.Enity;
namespace Commons.Interface
{
    public interface IRepository<TEntity>
    {



        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null);
        public IEnumerable<TEntity> GetAll();
        public void SaveChanges();
        public DataPage<T> QueryPage<T>(int pageNumber, int pageLength, Expression<Func<TEntity, bool>> filter = null);



    }
}
