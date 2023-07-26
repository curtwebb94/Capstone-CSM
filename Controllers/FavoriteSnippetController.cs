using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteSnippetController : ControllerBase
    {
        private readonly IFavoriteSnippetRepository _favoriteSnippetRepository;

        public FavoriteSnippetController(IFavoriteSnippetRepository favoriteSnippetRepository)
        {
            _favoriteSnippetRepository = favoriteSnippetRepository;
        }

        // GET: api/FavoriteSnippet
        [HttpGet]
        public ActionResult<IEnumerable<FavoriteSnippet>> GetAllFavoriteSnippets()
        {
            try
            {
                List<FavoriteSnippet> favoriteSnippets = _favoriteSnippetRepository.GetAllFavoriteSnippets();
                return Ok(favoriteSnippets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching favorite snippets.");
            }
        }

        // GET: api/FavoriteSnippet/{id}
        [HttpGet("{id}")]
        public ActionResult<FavoriteSnippet> GetFavoriteSnippetById(int id)
        {
            try
            {
                FavoriteSnippet favoriteSnippet = _favoriteSnippetRepository.GetFavoriteSnippetById(id);

                if (favoriteSnippet == null)
                {
                    return NotFound();
                }

                return Ok(favoriteSnippet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the favorite snippet.");
            }
        }

        // POST: api/FavoriteSnippet
        [HttpPost]
        public IActionResult AddFavoriteSnippet(FavoriteSnippet favoriteSnippet)
        {
            try
            {
                _favoriteSnippetRepository.AddFavoriteSnippet(favoriteSnippet);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the favorite snippet.");
            }
        }

        // PUT: api/FavoriteSnippet/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateFavoriteSnippet(int id, FavoriteSnippet favoriteSnippet)
        {
            try
            {
                FavoriteSnippet existingFavoriteSnippet = _favoriteSnippetRepository.GetFavoriteSnippetById(id);

                if (existingFavoriteSnippet == null)
                {
                    return NotFound();
                }

                favoriteSnippet.Id = id;
                _favoriteSnippetRepository.UpdateFavoriteSnippet(favoriteSnippet);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the favorite snippet.");
            }
        }

        // DELETE: api/FavoriteSnippet/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFavoriteSnippet(int id)
        {
            try
            {
                FavoriteSnippet existingFavoriteSnippet = _favoriteSnippetRepository.GetFavoriteSnippetById(id);

                if (existingFavoriteSnippet == null)
                {
                    return NotFound();
                }

                _favoriteSnippetRepository.DeleteFavoriteSnippet(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the favorite snippet.");
            }
        }
    }
}
