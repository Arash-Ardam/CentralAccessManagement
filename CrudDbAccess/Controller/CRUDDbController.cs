using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using CrudDbAccess.Repos.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDbAccess.Controller
{
    public class CRUDDbController : ApplicationControllerBase<IDbService>
    {
        public CRUDDbController(IDbService service) : base(service)
        {
        }


        [HttpPost("CreateDb")]
        public async Task<IActionResult> CreateDbAsync([FromBody] CreateDbDto createDatabase)
        {
            var isDouplicated = await Service.IsDbDouplicated(createDatabase.Address);

            if (isDouplicated)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "AlreadyExist" };

                return BadRequest(responseMessage);
            }

            var response = await Service.AddDbAsync(createDatabase);

            return Created(string.Empty, response);
        }

        [HttpPut("UpdateDb")]
        public async Task<IActionResult> UpdateDbAcync([FromBody] UpdateDbDto updateDbDto)
        {
            var isExist = await Service.IsDbExist(updateDbDto.Id);

            if (!isExist)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }

            var response = await Service.UpdateDbAsync(updateDbDto);

            return Accepted(response);
        }

        [HttpGet("GetAllDb")]
        public async Task<IActionResult> GetAllDbsAsync()
        {
            var response = await Service.GetAllDbsAsync();

            return Ok(response);
        }


        [HttpGet("GetDb/{id}")]
        public async Task<IActionResult> GetDbAsync([FromRoute] Guid id)
        {
            var isExist = await Service.IsDbExist(id);

            if (!isExist)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }

            var response = await Service.GetDbAsync(id);
            return Accepted(response);
        }

        [HttpGet("GetDbByAddress/{address}")]
        public async Task<IActionResult> GetDbByAddressAsync([FromRoute] string address)
        {
            //var isExist = await Service.IsDbExistByAddress(address);

            //if (!isExist)
            //{
            //    ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

            //    return BadRequest(responseMessage);
            //}

            var response = await Service.GetDbByAddressAsync(address);
            return Accepted(response);
        }

        [HttpGet("GetAllDbByZone/{zone}")]
        public async Task<IActionResult> GetDbByZoneAsync([FromRoute] Zone zone)
        {
            var response = await Service.GetDbsByZoneAsync(zone);
            return Accepted(response);
        }

        [HttpDelete("DeleteDb/{id}")]
        public async Task<IActionResult> DeleteDbAsync(Guid id)
        {
            var isExist = await Service.IsDbExist(id);

            if (!isExist)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }

            var response = await Service.DeleteDbAsync(id);
            return Accepted(response);
        }

        [HttpGet("EntryHandle")]
        public async Task<IActionResult> HandleEntrydataAsync()
        {
            var response = await Service.UpdateDataEntry();
            return Ok(response);
        }
    }
}
