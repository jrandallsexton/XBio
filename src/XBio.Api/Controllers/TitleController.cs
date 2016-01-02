using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using XBio.Core.Dtos;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/title")]
    public class TitleController : ApiController
    {
        [ResponseType(typeof(IEnumerable<KvpItem>))]
        [HttpGet]
        [Route("lookup")]
        public IHttpActionResult GetLookup()
        {
            return Ok(new TitleService().GetLookups());
        }
    }
}