using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBee.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBee.Controllers
{
    [Route("api/userEvents")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly IUserEventsRepository _userEventsRepository;

        public UserEventsController(IUserEventsRepository userEventsRepository)
        {
            _userEventsRepository = userEventsRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            var item = _userEventsRepository.GetUserEvents(id);
            if (item == null || item.Count == 0)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("event/{id}")]
        public IActionResult GetByEventId(int id)
        {
            var item = _userEventsRepository.GetEventUsers(id);
            if (item == null || item.Count == 0)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost("{id}/event/{eventId}")]
        public IActionResult Create(int id, int eventId)
        {
            var userEvent = _userEventsRepository.AddUserEvent(id, eventId);
            if (userEvent == null)
            {
                return BadRequest("Object not valid");
            }
            return Ok(userEvent);
        }


        [HttpDelete("{id}/event/{eventId}")]
        public IActionResult Delete(int id, int eventId)
        {
            var item = _userEventsRepository.DeleteUserEvent(id, eventId);
            if (item == false)
            {
                return BadRequest("Deletion failed");
            }

            return Ok(item);
        }
    }
}