
using Microsoft.EntityFrameworkCore;
namespace EFCoreWork.Interface
{
    public interface IProvider
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
