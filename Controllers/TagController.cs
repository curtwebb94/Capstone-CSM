using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: api/Tag
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetAllTags()
        {
            try
            {
                List<Tag> tags = _tagRepository.GetAllTags();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching tags.");
            }
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            try
            {
                Tag tag = _tagRepository.GetTagById(id);
                if (tag != null)
                {
                    return Ok(tag);
                }
                return NotFound(); // Return 404 Not Found if no tag with the given id is found.
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the tag.");
            }
        }

        // POST: api/Tag
        [HttpPost]
        public IActionResult AddTag([FromBody] Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);
                return CreatedAtAction("GetTagById", new { id = tag.Id }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the tag.");
            }
        }

        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, [FromBody] Tag tag)
        {
            try
            {
                Tag existingTag = _tagRepository.GetTagById(id);
                if (existingTag == null)
                {
                    return NotFound(); // Return 404 Not Found if no tag with the given id is found.
                }

                tag.Id = id; // Ensure the id of the tag to update matches the route parameter.
                _tagRepository.UpdateTag(tag);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the tag.");
            }
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                Tag existingTag = _tagRepository.GetTagById(id);
                if (existingTag == null)
                {
                    return NotFound(); // Return 404 Not Found if no tag with the given id is found.
                }

                _tagRepository.DeleteTag(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the tag.");
            }
        }
    }
}
