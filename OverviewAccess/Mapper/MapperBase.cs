using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;

namespace OverviewAccess.Mapper
{
    public class MapperBase : Profile 
    {

        public MapperBase()
        {
            CreateMap<CreateDbDto, DatabaseDetails>();
            CreateMap<DatabaseDetails,CreateDbDto>();
            CreateMap<UpdateDbDto, DatabaseDetails>();
            CreateMap<DatabaseDetails,UpdateDbDto>();
            CreateMap<DatabaseDetails,GetDbDto>();

            CreateMap<BaseAccessData, GetAccessDto>();
        }

    }
}
