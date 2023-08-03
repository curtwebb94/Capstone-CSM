using System;
using System.ComponentModel.DataAnnotations;

namespace CSM.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirebaseUserId { get; set; }

        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
