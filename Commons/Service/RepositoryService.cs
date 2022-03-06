using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Commons.Extention;
using Commons.Interface;
using Microsoft.EntityFrameworkCore;
using Commons.Profile.Enity;
using System.Collections;
using Data;
using AutoMapper;
namespace Commons.Service
{

    public class RepositoryService<Contexts, TEntity> : IRepository<TEntity> where Contexts : DbContext where TEntity : class
    {
        private Contexts _context;
        private readonly IMapper Mapper;

        public RepositoryService(Contexts context, IMapper _Mapper)
        {
            _context = context;
            Mapper = _Mapper;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        private IQueryable<TEntity> Querydb(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query.Where(filter);
            }
            return CheckNoTracking(query.ElementType.Name) ? query.AsNoTracking() : query;

        }

        private bool CheckNoTracking(string name)
        {
            if (name.Substring(0, 1).ToLower() == "v")
                return true;


            return false;
        }
        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null)
        {

            if (filter != null)
            {
                return Querydb().Where(filter).ToList();
            }
            else
            {
                return Querydb().ToList();
            }


        }
        private IEnumerable<TEntity> QueryPages(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
            {
                return Query(filter).Skip(startRow).Take(pageLength).ToList();
            }
            else
            {
                return Query(null).Skip(startRow).Take(pageLength).ToList();

            }
        }
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _context.Set<TEntity>();
            int itemcount = 0;
            if (filter != null)
            {
                itemcount = query.Where(filter).Count();
            }
            else
            {
                itemcount = query.Count();
            }
            return itemcount;
        }
        public DataPage<T> QueryPage<T>(int pageNumber, int pageLength, Expression<Func<TEntity, bool>> filter = null)
        {

            var startRow = (pageNumber - 1) * pageLength;
            var data = Mapper.Map<List<T>>(QueryPages(startRow, pageLength, filter).ToList());
            if (data == null)
            {
                data = new List<T>();
            }
            var totalCount = GetCount(filter);
            return CreateDataPage<T>(pageNumber, pageLength, data, totalCount);
        }
        private DataPage<T> CreateDataPage<T>(int pageNumber, int pageLength, dynamic data, long totalEntityCount)
        {
            var page = new DataPage<T>()
            {
                Data = data,
                TotalEntityCount = totalEntityCount,
                PageLength = pageLength,
                PageNumber = pageNumber
            };

            return page;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = Querydb(null);
            return result.ToList();
        }
    }
}
