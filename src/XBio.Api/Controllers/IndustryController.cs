
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Web.Http;

using XBio.Core.Dtos;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/industry")]
    public class IndustryController : ApiController
    {
        [ResponseType(typeof(IEnumerable<Select2Item>))]
        [HttpGet]
        [Route("lookup")]
        public IHttpActionResult Lookup()
        {
            return Ok(new IndustryService().Lookup());
        }
    }
}