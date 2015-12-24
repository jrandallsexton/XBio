
using System.Web.Http;
using System.Web.Http.Description;

using XBio.Core.Interfaces;
using XBio.Service;

namespace XBio.Api.Controllers
{

    [RoutePrefix("api/resumes")]
    public class ResumeController : ApiController
    {
        private ResumeService _service = new ResumeService();

        [ResponseType(typeof(IResume))]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

    }

}