using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OverviewAccess.Configurations;
using OverviewAccess.Models;
using System.Net;

namespace OverviewAccess.Controllers
{
    public class AccessController : AppControllerBase
    {
        public AccessController(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<ApiConfigurations> options) : base(httpClientFactory, mapper, options)
        {
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var route = CreateAccessRoute("GetAllAccess", "");
            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            var model = new AccessViewModel() { Accesses = message };

            return View("List", model);
        }

        [HttpPost]
        public async Task<FileResult> ExportExcel(AccessViewModel excelViewModel)
        {
            var fileDetails = await CreateFileNameAndData(excelViewModel);

            return GenerateExcel(fileDetails.fileName, fileDetails.fileData);

        }


        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAccessDto createAccessDto)
        {
            var route = CreateAccessRoute("CreateAccess", "");
            var response = await _httpclient.PostAsJsonAsync(route, createAccessDto);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<ResponseMessage>(jsonResponse, null);

            TempData["Message"] = message.Message;
            return await List();

        }

        [HttpGet]
        public IActionResult GetAccess()
        {
            return View("GetAccess");
        }

        [HttpPost]
        public async Task<IActionResult> GetAccess(Guid id)
        {
            var route = CreateAccessRoute("GetAccess", $"{id}");

            var response = await _httpclient.GetAsync(route);

            var message = JsonToObjectConvertor<BaseAccessData>(await CreateResponceMessage(response), null);

            return View("GetAccess", message);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessBySearch(AccessViewModel searchViewModel)
        {
            Uri route = SearchUriGenerator(searchViewModel);

            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            var model = new AccessViewModel() { Accesses = message };

            return View("List", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessByZone(Zone zone)
        {
            var route = CreateAccessRoute("GetAllAccessByZone", $"{zone}");
            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            var model = new AccessViewModel() { Accesses = message };

            return View("List", model);
        }

        [HttpGet("UpdateAccess/{id}")]
        public async Task<IActionResult> ToUpdate(Guid id)
        {
            var route = CreateAccessRoute("GetAccess", $"{id}");

            var response = await _httpclient.GetAsync(route);
            var t = await response.Content.ReadAsStringAsync();

            var AccessDetail = JsonToObjectConvertor<BaseAccessData>(await CreateResponceMessage(response), null);
            var message = new UpdateAccessDto()
            {
                Id = AccessDetail.Id,
                From = _mapper.Map<UpdateDbDto>(AccessDetail.From),
                To = _mapper.Map<UpdateDbDto>(AccessDetail.To),
                Port = AccessDetail.Port,
            };

            return View("Update", message);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAccessDto updateAccessDto)
        {
            var route = CreateAccessRoute("UpdateAccess", "");

            var response = await _httpclient.PutAsJsonAsync(route, updateAccessDto);

            var messageJson = await CreateResponceMessage(response);
            var responseMesssage = JsonToObjectConvertor<ResponseMessage>(messageJson, null);

            return View("DoneAction", responseMesssage);
        }

        [HttpGet("DeleteAccess/{id}")]
        public async Task<IActionResult> ToDelete(Guid id)
        {
            var route = CreateAccessRoute("GetAccess", $"{id}");

            var response = await _httpclient.GetAsync(route);

            var message = JsonToObjectConvertor<BaseAccessData>(await CreateResponceMessage(response), null);

            return View("Delete", message);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var route = CreateAccessRoute("DeleteAccess", $"{id}");

            await _httpclient.DeleteAsync(route);

            return RedirectToAction("List", "Access");
        }

    }
}
