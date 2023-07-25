using CSM.Models;
using System.Collections.Generic;

namespace CSM.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAllUserProfiles();
    }
}
