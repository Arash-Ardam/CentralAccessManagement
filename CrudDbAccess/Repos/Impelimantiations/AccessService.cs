using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using CrudDbAccess.Infrastructure;
using CrudDbAccess.Repos.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudDbAccess.Repos.Impelimantiations
{
    public class AccessService : ServiceBase, IAccessService
    {
        public AccessService(IMapper mapper, ApplicationDbContext dbContext) : base(mapper, dbContext)
        {
        }

        public async Task<ResponseMessage> AddAccessAsync(CreateAccessDto accessDto)
        {
            DatabaseDetails fromDb = await _context.InfraDatabases.FirstAsync(x => x.Address == accessDto.From.Address);
            DatabaseDetails toDb = await _context.InfraDatabases.FirstAsync(x => x.Address == accessDto.To.Address);

            BaseAccessData data = new BaseAccessData()
            {
                From = fromDb,
                To = toDb,
                Direction = accessDto.Direction,
                Port = accessDto.Port,
            };

            try
            {
                await _context.AccessInformation.AddAsync(data);

                await _context.SaveChangesAsync();

                return new ResponseMessage() { isSuccess = true, Message = "Created" };
            }
            catch (Exception ex)
            {

                return new ResponseMessage() { isSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResponseMessage> DeleteAccessAsync(Guid id)
        {
            var deletedEntity = await _context.AccessInformation.FindAsync(id);
            
            try
            {
                _context.AccessInformation.Remove(deletedEntity);
                _context.SaveChanges();
            
                return new ResponseMessage() { isSuccess = true, Message = "Deleted Successfuly" };
            }
            catch (Exception ex)
            {
            
                return new ResponseMessage() {isSuccess = false, Message = ex.Message };
            }
        }

        public async Task<GetAccessDto> GetAccessAsync(Guid id)
        {
            var access = await _context.AccessInformation.Include(model => model.From).Include(model => model.To).FirstOrDefaultAsync(x => x.Id == id);//.FindAsync(id);
                var response = _mapper.Map<GetAccessDto>(access);

                return response;
        }

       

        public async Task<ResponseMessage> UpdateAccessAsync(UpdateAccessDto updateAccessDto)
        {
            DatabaseDetails fromUpdatedDb = await _context.InfraDatabases.FirstOrDefaultAsync(entity => entity.Address == updateAccessDto.From.Address);
            DatabaseDetails toUpdatedDb = await _context.InfraDatabases.FirstOrDefaultAsync(entity => entity.Address == updateAccessDto.To.Address);



            BaseAccessData updatedData = new BaseAccessData()
            {
                Id = updateAccessDto.Id,
                From = fromUpdatedDb,
                To = toUpdatedDb,
                Direction = updateAccessDto.Direction,
                Port = updateAccessDto.Port,
            };


            //Update AccessInformation
            if (fromUpdatedDb != null && toUpdatedDb != null)
            {
                try
                {
                    _context.AccessInformation.Update(updatedData);

                    await _context.SaveChangesAsync();
                    return new ResponseMessage() { isSuccess = true, Message = "Successfuly Updated" };
                }
                catch (Exception ex)
                {

                    return new ResponseMessage() { isSuccess = false, Message = ex.Message };
                }
            }
            else
                return new ResponseMessage() { isSuccess = false, Message = "there is no Db(s) like request content" };
        }


        public async Task<BaseAccessData> BaseAccessDataMapper(CreateAccessDto accessDto)
        {
            var from = _mapper.Map<DatabaseDetails>(accessDto.From);
            var to = _mapper.Map<DatabaseDetails>(accessDto.To);

            var database = new BaseAccessData()
            {
                From = from,
                To = to,
                Port = accessDto.Port,
                Direction = accessDto.Direction
            };
            return database;
        }

        public async Task<bool> IsAccessExistAsync(Guid id)
        {
            var isExist = await _context.AccessInformation.AnyAsync(x => x.Id == id);
            return isExist;
        }

        public async Task<bool> IsAccessDouplicatedAsync(CreateAccessDto accessDto)
        {
            var AccessData = await BaseAccessDataMapper(accessDto);

            DatabaseDetails fromDb = await _context.InfraDatabases.FirstAsync(x => x.Address == AccessData.From.Address);
            DatabaseDetails toDb = await _context.InfraDatabases.FirstAsync(x => x.Address == AccessData.To.Address);


            bool isExist = await _context.AccessInformation.AnyAsync(x =>x.From == fromDb && x.To == toDb && x.Port == AccessData.Port);

            return isExist;
        }

        public async Task<string> UpdateAccessEntry()
        {
            var baseInfo = _context.BaseInfo.ToList();

            try
            {
                foreach (var item in baseInfo)
                {
                    var from = await _context.InfraDatabases.FirstOrDefaultAsync(x => x.Address == item.from);
                    var to = await _context.InfraDatabases.FirstOrDefaultAsync(x => x.Address == item.to);

                    var entry = new CreateAccessDto() { From = _mapper.Map<CreateDbDto>(from), To = _mapper.Map<CreateDbDto>(to), Port = item.port };
                    await AddAccessAsync(entry);
                }
                return "Done";
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<BaseAccessData>> GetAllAccessAsync()
        {
            var allAccess = await _context.AccessInformation.Include(x => x.From).Include(x=>x.To).ToListAsync();
            return allAccess;
        }

        public async Task<IEnumerable<BaseAccessData>> GetAllAccessByZoneAsync(Zone zone)
        {
            var allAccess = await _context.AccessInformation.Where(e => e.From.Zone == zone || e.To.Zone == zone)
                .Include(x => x.From).Include(x => x.To).ToListAsync();
            return allAccess;
        }

        public async Task<IEnumerable<BaseAccessData>> GetAllAccessByAddressAsync(string address)
        {
            var allAccess = await _context.AccessInformation.Where(e => e.From.Address == address || e.To.Address == address)
              .Include(x => x.From).Include(x => x.To).ToListAsync();
            return allAccess;
        }
    }
}
