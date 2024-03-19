using DI_Service_Lifetime.Models;
using DI_Service_Lifetime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace DI_Service_Lifetime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScopedGuidService _scopedGuidService1;
        private readonly IScopedGuidService _scopedGuidService2;

        private readonly ITransientGuidService _transientGuidService1;
        private readonly ITransientGuidService _transientGuidService2;

        private readonly ISingletonGuidService _singletonGuidService1;
        private readonly ISingletonGuidService _singletonGuidService2;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IScopedGuidService scopedguid1, IScopedGuidService scopeguid2,
                            ISingletonGuidService single1, ISingletonGuidService single2,
                            ITransientGuidService trans1, ITransientGuidService trans2)
        {
            _scopedGuidService1 = scopedguid1;
            _scopedGuidService2 = scopeguid2;
            _transientGuidService1 = trans1;
            _transientGuidService2 = trans2;
            _singletonGuidService1 = single1;
            _singletonGuidService2 = single2;
        }

        public IActionResult Index()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Transient 1 : {_transientGuidService1.GetGuid()}\n");
            sb.Append($"Transient 2 : {_transientGuidService2.GetGuid()}\n\n\n");
            sb.Append($"Scoped 1 : {_scopedGuidService1.GetGuid()}\n");
            sb.Append($"Scoped 2 : {_scopedGuidService2.GetGuid()}\n\n\n");
            sb.Append($"Singleton 1 : {_singletonGuidService1.GetGuid()}\n");
            sb.Append($"Singleton 2 : {_singletonGuidService2.GetGuid()}\n\n\n");

            return Ok(sb.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
