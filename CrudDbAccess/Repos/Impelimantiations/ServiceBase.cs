using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Infrastructure;
using CrudDbAccess.Mapper;
using CrudDbAccess.Repos.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudDbAccess.Repos.Impelimantiations
{
    public class ServiceBase : IServiceBase
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _context;

        public ServiceBase(IMapper mapper,ApplicationDbContext dbContext)
        {
                _mapper = mapper;
                _context = dbContext;
        }


        
    }
}
