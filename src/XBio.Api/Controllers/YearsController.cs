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

    [RoutePrefix("api/years")]
    public class YearsController : ApiController
    {
        [ResponseType(typeof(IEnumerable<SelectItem>))]
        [HttpGet]
        [Route("lookup")]
        public IHttpActionResult Get()
        {
            var values = new List<SelectItem>();
            for (int i=1999; i<=2016; i++)
            {
                values.Add(new SelectItem(i, i.ToString()));
            }
            return Ok(values);
        }
    }
}
