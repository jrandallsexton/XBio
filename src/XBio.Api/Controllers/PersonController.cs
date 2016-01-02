
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
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {

        [ResponseType(typeof(Person))]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPerson(int id)
        {
            return Ok(new PersonService().Get(id));
        }

        [ResponseType(typeof(PersonDto))]
        [HttpGet]
        [Route("{id:int}/detail")]
        public IHttpActionResult GetPersonDetails(int id)
        {
            return Ok(new PersonService().GetPersonDto(id));
        }

        [ResponseType(typeof(IEnumerable<KvpItem>))]
        [HttpGet]
        [Route("{personId:int}/position")]
        public IHttpActionResult Get(int personId)
        {
            return Ok(new PersonService().GetPositionsByPersonId(personId));
        }

        [ResponseType(typeof(Position))]
        [HttpPost]
        [Route("{personId:int}/position")]
        public IHttpActionResult CreatePosition(int personId, Position position)
        {
            position.PersonId = personId;
            new PersonService().SavePosition(position);
            var route = string.Format("api/person/{0}/position/{1}", personId, position.Id);
            return CreatedAtRoute(route, null, new PositionService().Get(position.Id));
        }

        [ResponseType(typeof(IPosition))]
        [HttpGet]
        [Route("{personId:int}/position/{positionId:int}")]
        public IHttpActionResult GetPosition(int personId, int positionId)
        {
            return Ok(new PositionService().Get(positionId));
        }

        [ResponseType(typeof(IPosition))]
        [HttpPut]
        [Route("{personId:int}/position/{positionId:int}")]
        public IHttpActionResult UpdatePosition(int personId, int positionId, Position position)
        {
            if (personId < 0)
                return BadRequest("Invalid personId");
            if (positionId < 0)
                return BadRequest("Invalid positionId");
            position.PersonId = personId;
            new PersonService().SavePosition(position);
            //return StatusCode(HttpStatusCode.Accepted);
            return Ok(position);
        }

        [HttpDelete]
        [Route("{personId:int}/position/{positionId:int}")]
        public IHttpActionResult UpdatePosition(int personId, int positionId)
        {
            new PersonService().DeletePosition(personId, positionId);
            return Ok();
        }

    }
}