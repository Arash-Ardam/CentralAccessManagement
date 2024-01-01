using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDbAccess.Controller
{
    [Route("CentralAccessManagement/api/v1/[controller]")]
    [ApiController]
    public class ApplicationControllerBase<TService> : ControllerBase
        where TService : class
    {
        protected TService Service { get; set; }
        protected IActionResult ActionResult { get; set; }


        public ApplicationControllerBase(TService service)
        {
            Service = service;
        }
    }
}
