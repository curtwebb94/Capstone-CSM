﻿using System.Collections.Generic;
using CSM.Models;

namespace CSM.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAllUserProfiles();
        UserProfile GetUserProfileById(int id);
        int AddUserProfile(UserProfile userProfile);

    }
}