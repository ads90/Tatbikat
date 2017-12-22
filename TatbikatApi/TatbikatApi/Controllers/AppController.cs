    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TatbikatBLL;
using TatbikatModels;

namespace TatbikatApi.Controllers
{
    public class AppController : ApiController
    {
        private AppService appSer = new AppService();

        [HttpGet]
        public IHttpActionResult GetAllApps()
        {
            List<App> allApps = appSer.GetAllApps();
            return Ok(new { Result = allApps });
        }

        [HttpPost]
        public async Task AddApp(App newApp)
        {
            await appSer.AddApp(newApp);
        }
    }
}
