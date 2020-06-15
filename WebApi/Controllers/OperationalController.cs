using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OperationalController : ApiController
    {
        [Route("api/refreshconfig")]
        [HttpOptions]
        public IHttpActionResult RefreshConfiguration()
        {
            try
            {
                ConfigurationManager.RefreshSection("AppSettings");
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}