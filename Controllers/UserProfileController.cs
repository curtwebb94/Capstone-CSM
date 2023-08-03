using System;
using System.Collections.Generic;
using CSM.Models;
using CSM.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserProfile>> GetAllUserProfiles()
        {
            try
            {
                List<UserProfile> userProfiles = _userProfileRepository.GetAllUserProfiles();
                return Ok(userProfiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching user profiles.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserProfile> GetUserProfileById(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<UserProfile> AddUserProfile(UserProfile userProfile)
        {
            try
            {
                int newUserId = _userProfileRepository.AddUserProfile(userProfile);
                userProfile.Id = newUserId;
                return CreatedAtAction(nameof(GetUserProfileById), new { id = newUserId }, userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the user profile.");
            }
        }

        [HttpGet("firebase/{firebaseUserId}")]
        public ActionResult<UserProfile> GetUserByFirebaseUserId(string firebaseUserId)
        {
            // Use the repository to get the user by firebaseUserId
            UserProfile user = _userProfileRepository.GetUserProfileByFirebaseUserId(firebaseUserId);

            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            return Ok(user); // Return the user data in the response
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetUserProfileByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserProfile(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest("Invalid ID provided.");
            }

            var existingUserProfile = _userProfileRepository.GetUserProfileById(id);
            if (existingUserProfile == null)
            {
                return NotFound("User profile not found.");
            }

            existingUserProfile.Username = userProfile.Username;
            existingUserProfile.Password = userProfile.Password;

            bool updated = _userProfileRepository.UpdateUserProfile(existingUserProfile);
            if (updated)
            {
                return Ok(existingUserProfile);
            }

            return StatusCode(500, "Failed to update user profile.");
        }

    }
}
