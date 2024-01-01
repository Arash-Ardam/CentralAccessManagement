using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;

namespace CrudDbAccess.Repos.RepoInterfaces
{
    public interface IAccessService : IServiceBase
    {
        Task<ResponseMessage> AddAccessAsync(CreateAccessDto accessDto);
        Task<ResponseMessage> DeleteAccessAsync(Guid id);
        Task<GetAccessDto> GetAccessAsync(Guid id);
        Task<IEnumerable<BaseAccessData>> GetAllAccessAsync();
        Task<IEnumerable<BaseAccessData>> GetAllAccessByZoneAsync(Zone zone);
        Task<IEnumerable<BaseAccessData>> GetAllAccessByAddressAsync(string address);
        Task<ResponseMessage> UpdateAccessAsync(UpdateAccessDto updateAccessDto);

        Task<bool> IsAccessDouplicatedAsync(CreateAccessDto accessDto);
        Task<bool> IsAccessExistAsync(Guid id);
        Task<string> UpdateAccessEntry();
    }
}
