
using System.Web.Http;

using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {

        [HttpGet]
        [Route("{id:int}/position")]
        public IHttpActionResult Get(int id)
        {
            return Ok(new PersonService().GetPositionsByPersonId(id));
        }
    }
}