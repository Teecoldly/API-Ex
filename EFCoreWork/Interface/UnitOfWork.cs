using System;
using Microsoft.EntityFrameworkCore;
using EFCoreWork.Helpers;
namespace EFCoreWork.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private IServiceProvider _IServiceProvider;

        public UnitOfWork(DbContext context, IServiceProvider IServiceProvider)
        {
            _context = context;
            _IServiceProvider = IServiceProvider;
        }
        public Repository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_IServiceProvider, _context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
