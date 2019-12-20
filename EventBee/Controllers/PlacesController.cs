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
    [Route("api/places")]
    [ApiController]
    //[Authorize]
    public class PlacesController : ControllerBase
    {
        

        private readonly IPlaceRepository _placeRepository;

        public PlacesController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        // GET: Places
        [HttpGet]
        public IActionResult Get()
        {
            var places = _placeRepository.GetAll();
            if (places == null || places.Count == 0)
            {
                return NotFound();
            }

            return Ok(places);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var place = _placeRepository.GetById(id);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Place place)
        {
            var newPlace = _placeRepository.Add(place);
            if (newPlace == null)
            {
                return BadRequest("Object not valid");
            }
            return Ok(newPlace);
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Place place)
        {

            var updatedPlace =  _placeRepository.Update(id, place);
            if (updatedPlace == null)
            {
                return NotFound();
            }
            return Ok(updatedPlace);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var place = _placeRepository.DeleteById(id);
            if (place == false)
            {
                return BadRequest("Deletion failed");
            }

            return Ok("Irasas istrintas sekmingai");
        }
    }
}
