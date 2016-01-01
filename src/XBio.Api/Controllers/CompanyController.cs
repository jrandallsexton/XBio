
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using XBio.Core.Interfaces;
using XBio.Core.Models;
using XBio.Service;

namespace XBio.Api.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {

        [ResponseType(typeof(Company))]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GET(int id)
        {
            return Ok(new CompanyService().Get(id));
        }

        [ResponseType(typeof(Company))]
        [HttpPost]
        public IHttpActionResult CreateAddress(ICompany company)
        {
            if (company == null)
                return BadRequest("company cannot be null");

            new CompanyService().Save(company);
            var route = string.Format("api/company/{0}", company.Id);
            return CreatedAtRoute(route, null, new CompanyService().Get(company.Id));
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdatePosition(int id, ICompany company)
        {
            if (id < 0)
                return BadRequest("Invalid id");

            new CompanyService().Save(company);
            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}