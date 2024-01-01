using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OverviewAccess.Configurations;
using OverviewAccess.Models;

namespace OverviewAccess.Controllers
{
    public class DbController : AppControllerBase
    {
        public DbController(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<ApiConfigurations> options) : base(httpClientFactory, mapper, options)
        {
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new CreateDbDto();
            return View("Add", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateDbDto createDbDto)
        {
            var route = CreateDbRoute("CreateDb", "");
            var response = await _httpclient.PostAsJsonAsync(route, createDbDto);

            var messageJson = await CreateResponceMessage(response);
            var responseMesssage = JsonToObjectConvertor<ResponseMessage>(messageJson, null);

            TempData["Message"] = responseMesssage.Message;
            return await List();

        }

        [HttpGet]
        public async Task<IActionResult> List()

        {
            var route = CreateDbRoute("GetAllDb", "");

            var response = await _httpclient.GetAsync(route);

            var Message = JsonToObjectConvertor<IEnumerable<DatabaseDetails>>(await CreateResponceMessage(response), null);

            var model = new DbViewModel() { Accesses = Message };

            return View("List", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetDbBySearch(DbViewModel dbViewModel)
        {
            var route = SearchDbUriGenerator(dbViewModel);

            var response = await _httpclient.GetAsync(route);
            var Message = JsonToObjectConvertor<IEnumerable<DatabaseDetails>>(await CreateResponceMessage(response), null);

            var model = new DbViewModel() { Accesses = Message };

            return View("List", model);
        }


        [HttpPost]
        public async Task<FileResult> ExportExcel(DbViewModel excelViewModel)
        {
            var fileDetails = await CreateDbFileNameAndData(excelViewModel);

            return GenerateDbExcel(fileDetails.fileName, fileDetails.fileData);

        }

        [HttpGet]
        public IActionResult GetDb()
        {
            return View("GetDb");
        }

        [HttpPost]
        public async Task<IActionResult> GetDb(string address)
        {
            var route = CreateDbRoute("GetDbByAddress", $"{address}");

            var response = await _httpclient.GetAsync(route);

            var message = JsonToObjectConvertor<DatabaseDetails>(await CreateResponceMessage(response), null);

            return View("GetDb", message);
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> ToUpdate(Guid id)
        {
            var route = CreateDbRoute("GetDb", $"{id}");

            var response = await _httpclient.GetAsync(route);

            var databaseDetail = JsonToObjectConvertor<DatabaseDetails>(await CreateResponceMessage(response), null);
            var message = _mapper.Map<UpdateDbDto>(databaseDetail);

            return View("Update", message);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDbDto updateDbDto)
        {
            var route = CreateDbRoute("UpdateDb", "");

            var response = await _httpclient.PutAsJsonAsync(route, updateDbDto);

            var messageJson = await CreateResponceMessage(response);
            var responseMesssage = JsonToObjectConvertor<ResponseMessage>(messageJson, null);

            return View("DoneAction", responseMesssage);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> ToDelete(Guid id)
        {
            var route = CreateDbRoute("GetDb", $"{id}");

            var response = await _httpclient.GetAsync(route);

            var message = JsonToObjectConvertor<DatabaseDetails>(await CreateResponceMessage(response), null);

            return View("Delete", message);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var route = CreateDbRoute("DeleteDb", $"{id}");

            await _httpclient.DeleteAsync(route);

            return RedirectToAction("List", "Db");
        }
    }
}
