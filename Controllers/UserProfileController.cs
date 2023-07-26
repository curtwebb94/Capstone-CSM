﻿using System;
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

        // You can add more CRUD operations (e.g., Update and Delete) here as needed.
    }
}