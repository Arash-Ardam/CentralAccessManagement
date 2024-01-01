using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using CrudDbAccess.Infrastructure;
using CrudDbAccess.Repos.RepoInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;

namespace CrudDbAccess.Repos.Impelimantiations
{
    public class DbService :  ServiceBase , IDbService
    {
        public DbService(IMapper mapper, ApplicationDbContext dbContext) : base(mapper, dbContext)
        {
        }

        public async Task<ResponseMessage> AddDbAsync(CreateDbDto createDatabase)
        {
            var isDouplicated = await IsDbDouplicated(createDatabase.Address);

            if (isDouplicated)
            {
                return new ResponseMessage() { isSuccess = false, Message = "DbDouplicated" };
            }
            else
            {
                var database = _mapper.Map<DatabaseDetails>(createDatabase);
                await _context.InfraDatabases.AddAsync(database);

                await _context.SaveChangesAsync();
                return new ResponseMessage() { isSuccess = true, Message = "Created" };
            }
        }

        public async Task<ResponseMessage> DeleteDbAsync(Guid id)
        {
            var deletedDb = await _context.InfraDatabases.FirstOrDefaultAsync(x => x.Id == id);
            var deletedAccesses = await _context.AccessInformation.Where(x => x.From.Id == id || x.To.Id ==id).ToListAsync();

            //Delete AccessInformation if exist
            _context.AccessInformation.RemoveRange(deletedAccesses);

            //Delete Db
            _context.InfraDatabases.Remove(deletedDb);

            await _context.SaveChangesAsync();

            return new ResponseMessage() {isSuccess = true, Message = "Deleted Successfuly"};
        }

        public async Task<IEnumerable<DatabaseDetails>> GetAllDbsAsync()
        {
            var response = await _context.InfraDatabases.ToListAsync();
            return response;
        }

        public async Task<GetDbDto> GetDbAsync(Guid id)
        {
            var entity = await _context.InfraDatabases.FindAsync(id);
            var response = _mapper.Map<GetDbDto>(entity);

            return response;
        }

        public async Task<bool> IsDbDouplicated(string address)
        {
            var isDouplicated= await _context.InfraDatabases.AnyAsync(x => x.Address == address);

            return isDouplicated;
        }

        public async Task<bool> IsDbExist(Guid Id)
        {
            var isExist = await _context.InfraDatabases.AnyAsync(x => x.Id == Id);

            return isExist;
        }

        public async Task<string> UpdateDataEntry()
        {
            var entryDb = FilterDbs();
            try
            {
                foreach (var db in entryDb.asiatec)
                {
                    foreach (var item in db)
                    {
                        var entrydata = new CreateDbDto() { Address = item, Zone = Zone.Asiatec };
                        await AddDbAsync(entrydata);
                    }
                }

                foreach (var db in entryDb.tebian)
                {
                    foreach (var item in db)
                    {
                        var entrydata = new CreateDbDto() { Address = item, Zone = Zone.Tebian };
                        await AddDbAsync(entrydata);
                    }
                }

                foreach (var db in entryDb.external)
                {
                    foreach (var item in db)
                    {
                        var entrydata = new CreateDbDto() { Address = item, Zone = Zone.External };
                        await AddDbAsync(entrydata);
                    }
                }
                return "Done";
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<ResponseMessage> UpdateDbAsync(UpdateDbDto updateDbDto)
        {
            try
            {
                var updatedDb = _mapper.Map<DatabaseDetails>(updateDbDto);
                _context.InfraDatabases.Update(updatedDb);

                await _context.SaveChangesAsync();
                return new ResponseMessage() { isSuccess = true, Message = "Successfuly Updated" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { isSuccess = false, Message = ex.Message};
            }
        }


        public(List<List<string>> asiatec , List<List<string>> tebian, List<List<string>> external) FilterDbs()
        {
            var asiatecDbFromIps = _context.BaseInfo.Where(x => x.from.Contains("172.28") || x.from.Contains("All")).Select(x => x.from).Distinct().ToList();
            var asiatecDbToIps = _context.BaseInfo.Where(x => x.to.Contains("172.28")).Select(x => x.to).Distinct().ToList();

            foreach (var item in asiatecDbFromIps)
            {
                if (asiatecDbToIps.Contains(item))
                {
                    asiatecDbToIps.Remove(item);
                }
            }
            var asiatec = new List<List<string>>() { asiatecDbFromIps , asiatecDbToIps };
            

            var tebianDbFromIps = _context.BaseInfo.Where(x => x.from.Contains("192.168") || x.from.Contains("All")).Select(x => x.from).Distinct().ToList();
            var tebianDbToIps = _context.BaseInfo.Where(x => x.to.Contains("192.168")).Select(x => x.to).Distinct().ToList();

            foreach (var item in tebianDbFromIps)
            {
                if (tebianDbToIps.Contains(item))
                {
                    tebianDbToIps.Remove(item);
                }
            }

            var tebian = new List<List<string>>() { tebianDbFromIps , tebianDbToIps };

            var asiatecDbExternal = _context.BaseInfo.Where(y => !y.to.Contains("172.28")).Select(y => y.to).Distinct().ToList();
            var tebianDbExternal = _context.BaseInfo.Where(y => !y.to.Contains("192.168")).Select(y => y.to).Distinct().ToList();

            foreach (var item in tebianDbExternal)
            {
                if (asiatecDbExternal.Contains(item))
                {
                    asiatecDbExternal.Remove(item);
                }
            }
            var external = new List<List<string>>() { asiatecDbExternal , tebianDbExternal };

            return (asiatec,tebian,external);

        }

        public async Task<GetDbDto> GetDbByAddressAsync(string address)
        {
            var entity = await _context.InfraDatabases.FirstOrDefaultAsync(x => x.Address == address);
            var result = _mapper.Map<GetDbDto>(entity);

            return result;
        }

        public async Task<bool> IsDbExistByAddress(string address)
        {
            var isExist = await _context.InfraDatabases.AnyAsync(x => x.Address == address);

            return isExist;
        }

        public async Task<IEnumerable<DatabaseDetails>> GetDbsByZoneAsync(Zone zone)
        {
            var entity = await _context.InfraDatabases.Where(x => x.Zone == zone).ToListAsync();

            return entity;
        }
    }
}
