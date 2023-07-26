using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSnippetTagController : ControllerBase
    {
        private readonly ICodeSnippetTagRepository _codeSnippetTagRepository;

        public CodeSnippetTagController(ICodeSnippetTagRepository codeSnippetTagRepository)
        {
            _codeSnippetTagRepository = codeSnippetTagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CodeSnippetTag>> GetAllCodeSnippetTags()
        {
            try
            {
                List<CodeSnippetTag> codeSnippetTags = _codeSnippetTagRepository.GetAllCodeSnippetTags();
                return Ok(codeSnippetTags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching code snippet tags.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CodeSnippetTag> GetCodeSnippetTagById(int id)
        {
            try
            {
                CodeSnippetTag codeSnippetTag = _codeSnippetTagRepository.GetCodeSnippetTagById(id);

                if (codeSnippetTag == null)
                {
                    return NotFound();
                }

                return Ok(codeSnippetTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the code snippet tag.");
            }
        }

        [HttpPost]
        public ActionResult<CodeSnippetTag> AddCodeSnippetTag(CodeSnippetTag codeSnippetTag)
        {
            try
            {
                _codeSnippetTagRepository.AddCodeSnippetTag(codeSnippetTag);
                return CreatedAtAction(nameof(GetCodeSnippetTagById), new { id = codeSnippetTag.Id }, codeSnippetTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the code snippet tag.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCodeSnippetTag(int id, CodeSnippetTag codeSnippetTag)
        {
            try
            {
                if (id != codeSnippetTag.Id)
                {
                    return BadRequest();
                }

                _codeSnippetTagRepository.UpdateCodeSnippetTag(codeSnippetTag);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the code snippet tag.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCodeSnippetTag(int id)
        {
            try
            {
                _codeSnippetTagRepository.DeleteCodeSnippetTag(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the code snippet tag.");
            }
        }
    }
}
