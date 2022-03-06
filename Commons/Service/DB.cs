using System;
using Commons.Extention;
using Commons.Service;
using Commons.Interface;
using AutoMapper;
namespace Commons.Service
{
    public class DB<T> where T : class
    {
        public ContextExtention _context;
        private readonly IMapper mapper;
        public DB(IMapper _mapper)
        {
            _context = new ContextExtention();
            mapper = _mapper;
        }
        public IRepository<T> Get()
        {
            return new RepositoryService<ContextExtention, T>(_context, mapper);
        }
    }
}
