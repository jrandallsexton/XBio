
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using XBio.Core.Dtos;
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
        public IHttpActionResult Get(int id)
        {
            return Ok(new CompanyService().Get(id));
        }

        [ResponseType(typeof(IEnumerable<KvpItem>))]
        [HttpGet]
        [Route("lookup")]
        public IHttpActionResult GetLookup()
        {
            return Ok(new CompanyService().GetLookups());
        }

        [ResponseType(typeof(IEnumerable<Select2Item>))]
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string q)
        {
            return Ok(new CompanyService().Search(q));
        }

        [ResponseType(typeof(Company))]
        [HttpPost]
        public IHttpActionResult Create(ICompany company)
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
        public IHttpActionResult Update(int id, ICompany company)
        {
            if (id < 0)
                return BadRequest("Invalid id");

            new CompanyService().Save(company);
            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}