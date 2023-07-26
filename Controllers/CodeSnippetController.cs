using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSnippetController : ControllerBase
    {
        private readonly ICodeSnippetRepository _codeSnippetRepository;

        public CodeSnippetController(ICodeSnippetRepository codeSnippetRepository)
        {
            _codeSnippetRepository = codeSnippetRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CodeSnippet>> GetAllCodeSnippets()
        {
            try
            {
                List<CodeSnippet> codeSnippets = _codeSnippetRepository.GetAllCodeSnippets();
                return Ok(codeSnippets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching code snippets.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CodeSnippet> GetCodeSnippetById(int id)
        {
            try
            {
                CodeSnippet codeSnippet = _codeSnippetRepository.GetCodeSnippetById(id);

                if (codeSnippet != null)
                {
                    return Ok(codeSnippet);
                }

                return NotFound(); // Return 404 if code snippet with the given id is not found.
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching code snippet.");
            }
        }

        [HttpPost]
        public ActionResult<CodeSnippet> AddCodeSnippet(CodeSnippet codeSnippet)
        {
            try
            {
                _codeSnippetRepository.AddCodeSnippet(codeSnippet);
                return CreatedAtAction(nameof(GetCodeSnippetById), new { id = codeSnippet.Id }, codeSnippet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding code snippet.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCodeSnippet(int id, CodeSnippet codeSnippet)
        {
            try
            {
                CodeSnippet existingCodeSnippet = _codeSnippetRepository.GetCodeSnippetById(id);

                if (existingCodeSnippet == null)
                {
                    return NotFound(); // Return 404 if code snippet with the given id is not found.
                }

                codeSnippet.Id = id;
                _codeSnippetRepository.EditCodeSnippet(codeSnippet);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while editing code snippet.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCodeSnippet(int id)
        {
            try
            {
                CodeSnippet existingCodeSnippet = _codeSnippetRepository.GetCodeSnippetById(id);

                if (existingCodeSnippet == null)
                {
                    return NotFound(); // Return 404 if code snippet with the given id is not found.
                }

                _codeSnippetRepository.DeleteCodeSnippet(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting code snippet.");
            }
        }
    }
}
