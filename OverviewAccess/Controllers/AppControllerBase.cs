using AutoMapper;
using ClosedXML.Excel;
using CrudDbAccess.Data;
using CrudDbAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OverviewAccess.Configurations;
using OverviewAccess.Models;
using System.Data;

namespace OverviewAccess.Controllers
{
    public class AppControllerBase : Controller
    {
        private readonly IHttpClientFactory _factory;
        protected readonly HttpClient _httpclient;
        protected readonly IMapper _mapper;
        private readonly IOptions<ApiConfigurations> _options;

        public AppControllerBase(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<ApiConfigurations> options)
        {
            _options = options;
            _factory = httpClientFactory;
            _httpclient = _factory.CreateClient();
            _httpclient.BaseAddress = new Uri(_options.Value.BaseUrl, uriKind: UriKind.Absolute);
            _mapper = mapper;            
        }

        protected Uri CreateAccessRoute(string action, string route)
        {
            return new Uri($"{_httpclient.BaseAddress}/api/v1/CRUDAccess/{action}/{route}");
        }

        protected Uri CreateDbRoute(string action, string route)
        {
            return new Uri($"{_httpclient.BaseAddress}/api/v1/CRUDDb/{action}/{route}");
        }

        protected async Task<string> CreateResponceMessage(HttpResponseMessage httpResponse)
        {
            return await httpResponse.Content.ReadAsStringAsync();
        }

        protected requestType JsonToObjectConvertor<requestType>(string file, string? section) where requestType : class
        {
            if (null == section)
            {
                return JsonConvert.DeserializeObject<requestType>(file);
            }
            var body = JsonConvert.DeserializeObject<requestType>(JObject.Parse(file)[section].ToString());

            return body;
        }

        protected Uri SearchUriGenerator(AccessViewModel searchViewModel)
        {
            Uri route;
            if (searchViewModel.Zone != null)
            {
                route = CreateAccessRoute("GetAllAccessByZone", $"{searchViewModel.Zone}");
            }
            else if (searchViewModel.Address != null)
            {
                route = CreateAccessRoute("GetAllAccessByAddress", $"{searchViewModel.Address}");
            }
            else
            {
                route = CreateAccessRoute("GetAllAccess", "");
            }
            return route;
        }

        protected Uri SearchGraphUriGenerator(GraphViewModel graphViewModel)
        {
            Uri route;
            if (graphViewModel.SelectedZone != null)
            {
                route = CreateAccessRoute("GetAllAccessByZone", $"{graphViewModel.SelectedZone}");
            }
            else if (graphViewModel.Address != null)
            {
                route = CreateAccessRoute("GetAllAccessByAddress", $"{graphViewModel.Address}");
            }
            else
            {
                route = CreateAccessRoute("GetAllAccess", "");
            }
            return route;
        }

        protected Uri SearchDbUriGenerator(DbViewModel dbViewModel)
        {
            Uri route;
            if (dbViewModel.Zone != null)
            {
                route = CreateDbRoute("GetAllDbByZone", $"{dbViewModel.Zone}");
            }
            else if (dbViewModel.Address != null)
            {
                route = CreateDbRoute("GetDbByAddress", $"{dbViewModel.Address}");
            }
            else
            {
                route = CreateDbRoute("GetAllDb", "");
            }
            return route;
        }

        protected async Task<(string fileName, IEnumerable<BaseAccessData> fileData)> CreateFileNameAndData(AccessViewModel excelViewModel)
        {
            var fileName = string.Empty;
            IEnumerable<BaseAccessData> accessData;

            if (excelViewModel.Zone != null)
            {
                fileName = $"{excelViewModel.Zone}.xlsx";
                var route = CreateAccessRoute("GetAllAccessByZone", $"{excelViewModel.Zone}");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }
            else if (excelViewModel.Address != null)
            {
                fileName = $"{excelViewModel.Address}.xlsx";
                var route = CreateAccessRoute("GetAllAccessByAddress", $"{excelViewModel.Address}");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }
            else
            {
                fileName = "AllAccesses.xlsx";
                var route = CreateAccessRoute("GetAllAccess", "");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }

            return (fileName, accessData);
        }

        protected async Task<(string fileName, IEnumerable<DatabaseDetails> fileData)> CreateDbFileNameAndData(DbViewModel dbViewModel)
        {
            var fileName = string.Empty;
            IEnumerable<DatabaseDetails> accessData;

            if (dbViewModel.Zone != null)
            {
                fileName = $"{dbViewModel.Zone}Dbs.xlsx";
                var route = CreateDbRoute("GetAllDbByZone", $"{dbViewModel.Zone}");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<DatabaseDetails>>(jsonResponse, null);
            }
            else
            {
                fileName = "AllDbs.xlsx";
                var route = CreateDbRoute("GetAllDb", "");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<DatabaseDetails>>(jsonResponse, null);
            }

            return (fileName, accessData);
        }



        protected async Task<(string fileName, IEnumerable<BaseAccessData> fileData)> CreateGraphFileNameAndData(GraphViewModel graphViewModel)
        {
            var fileName = string.Empty;
            IEnumerable<BaseAccessData> accessData;

            if (graphViewModel.SelectedZone != null)
            {
                fileName = $"{graphViewModel.SelectedZone}.xlsx";
                var route = CreateAccessRoute("GetAllAccessByZone", $"{graphViewModel.SelectedZone}");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }
            else if (graphViewModel.Address != null)
            {
                fileName = $"{graphViewModel.Address}.xlsx";
                var route = CreateAccessRoute("GetAllAccessByAddress", $"{graphViewModel.Address}");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }
            else
            {
                fileName = "AllAccesses.xlsx";
                var route = CreateAccessRoute("GetAllAccess", "");
                var response = await _httpclient.GetAsync(route);
                var jsonResponse = await CreateResponceMessage(response);

                accessData = JsonToObjectConvertor<IEnumerable<BaseAccessData>>(jsonResponse, null);
            }

            return (fileName, accessData);
        }


        protected FileResult GenerateExcel(string fileName, IEnumerable<BaseAccessData> AccessDatas)
        {
            DataTable dataTable = new DataTable("AccessDatas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("From"),
                new DataColumn("To"),
                new DataColumn("Port")

            });

            foreach (var item in AccessDatas)
            {
                dataTable.Rows.Add(item.From.Address, item.To.Address, item.Port);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        protected FileResult GenerateDbExcel(string fileName, IEnumerable<DatabaseDetails> AccessDatas)
        {
            DataTable dataTable = new DataTable("DbDatas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Address"),
                new DataColumn("Zone"),
                new DataColumn("Name")

            });

            foreach (var item in AccessDatas)
            {
                dataTable.Rows.Add(item.Address, item.Zone, item.Name);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

    }

}

