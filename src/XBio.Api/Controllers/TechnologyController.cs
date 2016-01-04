
using System.Collections.Generic;

using System.Web.Http;
using System.Web.Http.Description;
using XBio.Core.Dtos;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/technology")]
    public class TechnologyController : ApiController
    {
        [ResponseType(typeof(IEnumerable<KvpItem>))]
        [HttpGet]
        [Route("lookup/{technologyTypeId:int}")]
        public IHttpActionResult Get(int technologyTypeId)
        {
            return Ok(new TechnologyService().GetTechnologiesByType(technologyTypeId));
        }
    }
}