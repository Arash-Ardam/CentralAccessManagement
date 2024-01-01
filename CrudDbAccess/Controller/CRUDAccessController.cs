using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using CrudDbAccess.Repos.Impelimantiations;
using CrudDbAccess.Repos.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CrudDbAccess.Controller
{
    public class CRUDAccessController : ApplicationControllerBase<IAccessService>
    {
        public CRUDAccessController(IAccessService service) : base(service)
        {
        }

        [HttpPost("CreateAccess")]
        public async Task<IActionResult> CreateAccessAsync([FromBody] CreateAccessDto accessDto)
        {
            var isExist = await Service.IsAccessDouplicatedAsync(accessDto);

            if (isExist)
            {
                var response = new ResponseMessage() { isSuccess = false, Message = "AlreadyExist" };
                ActionResult = BadRequest(response);
                return ActionResult;
            }

            var result = await Service.AddAccessAsync(accessDto);

            return Created(string.Empty,result);  
        }

        [HttpPut("UpdateAccess")]
        public async Task<IActionResult> UpdateAccessAsync([FromBody] UpdateAccessDto updateAccessDto)
        {
            var isExist = await Service.IsAccessExistAsync(updateAccessDto.Id);

            if (!isExist) 
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }
             
            var result = await Service.UpdateAccessAsync(updateAccessDto);

            switch (result.isSuccess)
            {
                case true:
                    return Accepted(result);
                case false:
                    return BadRequest(result);
            }
        }

        [HttpGet("GetAccess/{id}")]
        public async Task<IActionResult> GetAccessAsync([FromRoute] Guid id)
        {
            var isExist = await Service.IsAccessExistAsync(id);

            if (!isExist)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }

            var result = await Service.GetAccessAsync(id);

            return Accepted(result);
        }

        [HttpGet("GetAllAccess")]
        public async Task<IActionResult> GetAllAccessAsync()
        {
            var response = await Service.GetAllAccessAsync();
            return Ok(response);
        }

        [HttpGet("GetAllAccessByZone/{zone}")]
        public async Task<IActionResult> GetAllAccessAsyncByZone(Zone zone)
        {
            var response = await Service.GetAllAccessByZoneAsync(zone);
            return Ok(response);
        }

        [HttpGet("GetAllAccessByAddress/{address}")]
        public async Task<IActionResult> GetAllAccessAsyncByAddress(string address)
        {
            var response = await Service.GetAllAccessByAddressAsync(address);
            return Ok(response);
        }

        [HttpDelete("DeleteAccess/{id}")]
        public async Task<IActionResult> DeleteAccessAsync([FromRoute] Guid id)
        {
            var isExist = await Service.IsAccessExistAsync(id);

            if (!isExist)
            {
                ResponseMessage responseMessage = new ResponseMessage() { isSuccess = false, Message = "Data is not exist" };

                return BadRequest(responseMessage);
            }

            var result = await Service.DeleteAccessAsync(id);

            return Accepted(result);
        }

        [HttpGet("HandleAccessEntry")]
        public async Task<IActionResult> HandleAccessEntryAsync()
        {
            var response = await Service.UpdateAccessEntry();
            return Ok(response);
        }
       
    }
}
