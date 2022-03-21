using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using EFCoreWork.Helpers;
using DataDB;
namespace EFCoreWork.Interface
{
    public class UowProvider : IProvider
    {

        private IServiceProvider _IServiceProvider;
        private testdbContext _Context;



        public UowProvider(IServiceProvider IServiceProvider, testdbContext DBContexts)
        {

            _IServiceProvider = IServiceProvider;
            _Context = DBContexts;
        }


        public IUnitOfWork CreateUnitOfWork()
        {

            return new UnitOfWork(_Context, _IServiceProvider);
        }
    }

}
