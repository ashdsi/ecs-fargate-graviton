using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graviton.Controllers
{

    public class EventsController : Controller
    {

        // GET: Path=/events
        [HttpGet]
        [Route("events")]
        public ActionResult Index()
        {
            var systeminfo = new sysinfo
            {
                OSDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                OSArchitecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString(),
                FrameworkDescription = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
                ProcessArchitecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString(),
                RuntimeIdentifier = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier, 
            };
            return Ok(systeminfo);

        }

        // GET: Path=/
        [HttpGet]
        [Route("/")]
        public ActionResult Health()
        {
            return Ok("Healthy");

        }
    }
}
