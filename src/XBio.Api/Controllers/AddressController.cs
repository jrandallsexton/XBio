
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using Microsoft.Owin.BuilderProperties;

using XBio.Core.Interfaces;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/address")]
    public class AddressController : ApiController
    {

        [ResponseType(typeof(Address))]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GET(int id)
        {
            return Ok(new AddressService().Get(id));
        }

        [ResponseType(typeof(Address))]
        [HttpPost]
        public IHttpActionResult CreateAddress(IAddress address)
        {
            if (address == null)
                return BadRequest("address cannot be null");

            new AddressService().Save(address);
            var route = string.Format("api/address/{0}", address.Id);
            return CreatedAtRoute(route, null, new AddressService().Get(address.Id));
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdatePosition(int id, IAddress address)
        {
            if (id < 0)
                return BadRequest("Invalid id");

            new AddressService().Save(address);
            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}