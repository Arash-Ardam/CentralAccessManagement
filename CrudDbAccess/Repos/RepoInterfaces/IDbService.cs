using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;

namespace CrudDbAccess.Repos.RepoInterfaces
{
    public interface IDbService : IServiceBase
    {
        Task<ResponseMessage> AddDbAsync(CreateDbDto createDatabase);
        Task<ResponseMessage> UpdateDbAsync(UpdateDbDto updateDbDto);
        Task<GetDbDto> GetDbAsync(Guid id);
        Task<GetDbDto> GetDbByAddressAsync(string address);
        Task<IEnumerable<DatabaseDetails>> GetDbsByZoneAsync(Zone zone);
        Task<ResponseMessage> DeleteDbAsync(Guid id);

        Task<IEnumerable<DatabaseDetails>>GetAllDbsAsync();


        Task<bool> IsDbDouplicated(string address);
        Task<bool> IsDbExist(Guid Id);
        Task<bool> IsDbExistByAddress(string address);
        Task<string> UpdateDataEntry();
    }
}
