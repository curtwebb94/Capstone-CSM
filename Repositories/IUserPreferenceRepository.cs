using System.Collections.Generic;
using CSM.Models;

namespace CSM.Repositories
{
    public interface IUserPreferenceRepository
    {
        List<UserPreference> GetAllUserPreferences();
        UserPreference GetByUserId(int userId);
        void AddUserPreference(UserPreference userPreference);
        void UpdateUserPreference(UserPreference userPreference);
        void DeleteUserPreference(int id);
    }
}
