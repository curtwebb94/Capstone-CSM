using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferenceController : ControllerBase
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public UserPreferenceController(IUserPreferenceRepository userPreferenceRepository)
        {
            _userPreferenceRepository = userPreferenceRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserPreference>> GetAllUserPreferences()
        {
            try
            {
                List<UserPreference> userPreferences = _userPreferenceRepository.GetAllUserPreferences();
                return Ok(userPreferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching user preferences.");
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<UserPreference> GetUserPreferenceByUserId(int userId)
        {
            try
            {
                UserPreference userPreference = _userPreferenceRepository.GetByUserId(userId);
                if (userPreference != null)
                {
                    return Ok(userPreference);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching user preference.");
            }
        }

        [HttpPost]
        public ActionResult<UserPreference> AddUserPreference(UserPreference userPreference)
        {
            try
            {
                _userPreferenceRepository.AddUserPreference(userPreference);
                return CreatedAtAction(nameof(GetUserPreferenceByUserId), new { userId = userPreference.UserId }, userPreference);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding user preference.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserPreference(int id, UserPreference userPreference)
        {
            try
            {
                if (id != userPreference.Id)
                {
                    return BadRequest();
                }

                _userPreferenceRepository.UpdateUserPreference(userPreference);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating user preference.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserPreference(int id)
        {
            try
            {
                _userPreferenceRepository.DeleteUserPreference(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting user preference.");
            }
        }
    }
}
