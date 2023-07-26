using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAllUserProfiles()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [Username], [Email], [Password], [CreateTime] FROM [User]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<UserProfile> userProfiles = new List<UserProfile>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int emailColumnPosition = reader.GetOrdinal("Email");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string emailValue = reader.GetString(emailColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                Username = usernameValue,
                                Email = emailValue,
                                Password = passwordValue,
                                CreateTime = createTimeValue
                            };

                            userProfiles.Add(userProfile);
                        }
                        return userProfiles;
                    }
                }
            }
        }

        public UserProfile GetUserProfileById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [Username], [Email], [Password], [CreateTime] FROM [User] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int emailColumnPosition = reader.GetOrdinal("Email");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string emailValue = reader.GetString(emailColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                Username = usernameValue,
                                Email = emailValue,
                                Password = passwordValue,
                                CreateTime = createTimeValue
                            };

                            return userProfile;
                        }
                        return null; // Return null if no user profile with the given id is found.
                    }
                }
            }
        }
        public int AddUserProfile(UserProfile userProfile)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    INSERT INTO [User] ([Username], [Email], [Password], [CreateTime])
                    VALUES (@Username, @Email, @Password, @CreateTime);
                    SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@Username", userProfile.Username);
                    command.Parameters.AddWithValue("@Email", userProfile.Email);
                    command.Parameters.AddWithValue("@Password", userProfile.Password);
                    command.Parameters.AddWithValue("@CreateTime", userProfile.CreateTime);

                    // ExecuteScalar is used to retrieve the generated identity value after insertion.
                    object result = command.ExecuteScalar();

                    // Check if the insertion was successful and return the newly generated ID, or -1 if failed.
                    return (result != null && int.TryParse(result.ToString(), out int id)) ? id : -1;
                }
            }
        }
    }
}
