using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CSM.Repositories;
using CSM.Models;

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
    }
}
