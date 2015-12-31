
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

using XBio.Core.Dtos;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {

        [ResponseType(typeof(PersonDto))]
        [HttpGet]
        [Route("{id:int}/detail")]
        public IHttpActionResult GetPersonDetails(int id)
        {
            return Ok(new PersonService().GetPersonDto(id));
        }

        [ResponseType(typeof(IEnumerable<KvpItem>))]
        [HttpGet]
        [Route("{id:int}/position")]
        public IHttpActionResult Get(int id)
        {
            return Ok(new PersonService().GetPositionsByPersonId(id));
        }
    }
}