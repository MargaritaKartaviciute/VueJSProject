using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventBee.Data;
using EventBee.Models;
using EventBee.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EventBee.Controllers
{
    [Route("api/events")]
    [ApiController]
    //[Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: Places
        [HttpGet]
        public IActionResult Get()
        {
            var events = _eventRepository.GetAll();
            if (events == null || events.Count == 0)
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _eventRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]  Event item)
        {
            var newEvent = _eventRepository.Add(item);
            if (newEvent == null)
            {
                return BadRequest("Object not valid");
            }
            return Ok(newEvent);
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Event item)
        {

            var updatedEvent = _eventRepository.Update(id, item);
            if (updatedEvent == null)
            {
                return NotFound();
            }
            return Ok(updatedEvent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _eventRepository.DeleteById(id);
            if (item == false)
            {
                return BadRequest("Deletion failed");
            }

            return Ok(item);
        }
    }
}
