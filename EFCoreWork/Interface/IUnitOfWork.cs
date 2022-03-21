using System;
using EFCoreWork.Helpers;
namespace EFCoreWork.Interface
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Repository<TEntity> GetRepository<TEntity>() where TEntity : class;

    }
}
