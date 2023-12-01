using API_CSharp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public List<Event> Events = new List<Event> {
            new (1, "Event A", "description 1", new DateTime(2023, 12, 12)),
            new (2, "Event B", "description 2", new DateTime(2024, 2, 14)),
            new (3, "Event C", "description 3", new DateTime(2023, 12, 24)),
            new (4, "Event D", "description 4", new DateTime(2024, 1, 5))
        };

        private int GetLastIndex() {
            int index = 0;

            foreach (Event e in Events)
            {
                if (e.Id > index)
                    index = e.Id;
            }

            return index;
        }

        // GET: api/<EventController>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return Events;
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Event Get(int id)
        {
            foreach (Event e in Events)
            {
                if (e.Id == id)
                    return e;
            }

            throw new Exception("Id Event doesn't exist");
        }

        // POST api/<EventController>
        [HttpPost]
        public dynamic Post(Event value)
        {
            value.Id = GetLastIndex() + 1;
            Events.Add(value);

            return new
            {
                success = true,
                message = "Event added",
                result = value
            };
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, Event value)
        {
            foreach(Event e in Events)
            {
                if (e.Id == id)
                {
                    e.Name = value.Name;
                    e.Description = value.Description;
                    e.Date = value.Date;
                }
            }
        }

        // DELETE api/<EventController>/5
        [HttpDelete]
        [Authorize]
        public dynamic Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var answerToken = Jwt.IsValidToken(identity);

            if (!answerToken.success) return answerToken;

            User user = answerToken.result;

            if (user.Rol != "admin")
            {
                return new
                {
                    success = false,
                    message = "Access denied to execute this action",
                    result = string.Empty
                };
            }

            Event? eventToFind = Events.Find(x => x.Id == id);

            if (eventToFind == null)
            {
                return new
                {
                    success = false,
                    message = "Event doesn't exist. Couldn't be deleted"
                };
            }

            Events.Remove(eventToFind);

            return new
            {
                success = true,
                message = "Event has been deleted successfully"
            };
        }
    }
}
