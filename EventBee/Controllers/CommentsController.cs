using EventBee.Data.Repositories.Interfaces;
using EventBee.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBee.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: Places
        [HttpGet]
        public IActionResult Get()
        {
            var comments = _commentRepository.GetAll();
            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _commentRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]  Comment item)
        {
            var newComment = _commentRepository.Add(item);
            if (newComment == null)
            {
                return BadRequest("Object not valid");
            }
            return Ok(newComment);
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Comment item)
        {

            var updatedComment = _commentRepository.Update(id, item);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _commentRepository.DeleteById(id);
            if (item == false)
            {
                return BadRequest("Deletion failed");
            }

            return Ok(item);
        }
    }
}