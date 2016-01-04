
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
        [Route("{personId:int}/position", Name="CreatePosition")]
        public IHttpActionResult CreatePosition(int personId, Position position)
        {
            position.PersonId = personId;
            new PersonService().SavePosition(position);
            return CreatedAtRoute("GetPosition", new { controller = "person", personId = position.PersonId, positionId = position.Id }, position);
        }

        [ResponseType(typeof(Position))]
        [HttpGet]
        [Route("{personId:int}/position/{positionId:int}", Name="GetPosition")]
        public IHttpActionResult GetPosition(int personId, int positionId)
        {
            if (personId < 0)
                return BadRequest("invalid personId");
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

        [ResponseType(typeof(bool))]
        [HttpDelete]
        [Route("{personId:int}/position/{positionId:int}")]
        public IHttpActionResult UpdatePosition(int personId, int positionId)
        {
            new PersonService().DeletePosition(positionId);
            return Ok(true);
        }

        [ResponseType(typeof(IEnumerable<Skill>))]
        [HttpGet]
        [Route("{personId:int}/skill")]
        public IHttpActionResult GetSkills(int personId)
        {
            return Ok(new PersonService().GetSkills(personId));
        }

        [ResponseType(typeof(bool))]
        [HttpPut]
        [Route("{personId:int}/skill")]
        public IHttpActionResult UpdateSkills(int personId, List<Skill> skills)
        {
            if (personId < 0)
                return BadRequest("invalid personId");

            new PersonService().SaveSkills(skills);
            return Ok(true);
        }
    }
}