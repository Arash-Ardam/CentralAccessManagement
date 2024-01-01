using AutoMapper;
using CrudDbAccess.Data;
using CrudDbAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OverviewAccess.Configurations;
using OverviewAccess.Models;

namespace OverviewAccess.Controllers
{
    public class GraphController : AppControllerBase
    {
        public GraphController(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<ApiConfigurations> options) : base(httpClientFactory, mapper, options)
        {
        }

        public async Task<IActionResult> GraphAccess()
        {
            var route = CreateAccessRoute("GetAllAccess", "");
            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            var graphViewModel = new GraphViewModel() { Accesses = message };

            return View("Graph", graphViewModel);
        }

        [HttpGet]
        public IActionResult GraphAccessBy()
        {
            return View("specifiedGraph", new GraphViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GraphAccessByZone(GraphViewModel viewModel)
        {
            var route = CreateAccessRoute("GetAllAccessByZone", $"{viewModel.SelectedZone}");
            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            viewModel.Accesses = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);

            return View("specifiedGraph", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GraphAccessByAddress(GraphViewModel viewModel)
        {
            var route = CreateAccessRoute("GetAllAccessByAddress", $"{viewModel.Address}");
            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            viewModel.Accesses = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);

            return View("specifiedGraph", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> GetGraphBySearch(GraphViewModel graphViewModel)
        {
            Uri route = SearchGraphUriGenerator(graphViewModel);

            var response = await _httpclient.GetAsync(route);
            var jsonResponse = await CreateResponceMessage(response);

            var message = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            var model = new GraphViewModel() { Accesses = message };

            return View("Graph", model);
        }

        [HttpPost]
        public async Task<FileResult> ExportExcel(GraphViewModel graphViewModel)
        {
            var fileDetails = await CreateGraphFileNameAndData(graphViewModel);

            return GenerateExcel(fileDetails.fileName, fileDetails.fileData);

        }

    }

    
}
