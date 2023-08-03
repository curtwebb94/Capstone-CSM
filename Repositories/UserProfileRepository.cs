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
                    command.CommandText = "SELECT [Id], [FirebaseUserId], [Username], [Password], [CreateTime] FROM [User]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<UserProfile> userProfiles = new List<UserProfile>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int firebaseUserIdColumnPosition = reader.GetOrdinal("FirebaseUserId");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string firebaseUserIdValue = reader.GetString(firebaseUserIdColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                FirebaseUserId = firebaseUserIdValue,
                                Username = usernameValue,
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
                    command.CommandText = "SELECT [Id], [FirebaseUserId], [Username], [Password], [CreateTime] FROM [User] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int firebaseUserIdColumnPosition = reader.GetOrdinal("FirebaseUserId");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string firebaseUserIdValue = reader.GetString(firebaseUserIdColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                FirebaseUserId = firebaseUserIdValue,
                                Username = usernameValue,
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

        public bool UpdateUserProfile(UserProfile userProfile)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE [User]
                        SET [Username] = @Username
                        WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Username", userProfile.Username);
                    command.Parameters.AddWithValue("@Id", userProfile.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
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
                    INSERT INTO [User] ([FirebaseUserId], [Username], [Password], [CreateTime])
                    VALUES (@FirebaseUserId, @Username, @Password, @CreateTime);
                    SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@FirebaseUserId", userProfile.FirebaseUserId);
                    command.Parameters.AddWithValue("@Username", userProfile.Username);
                    command.Parameters.AddWithValue("@Password", userProfile.Password);
                    command.Parameters.AddWithValue("@CreateTime", DateTime.Now);

                    // ExecuteScalar is used to retrieve the generated identity value after insertion.
                    object result = command.ExecuteScalar();

                    // Check if the insertion was successful and return the newly generated ID, or -1 if failed.
                    return (result != null && int.TryParse(result.ToString(), out int id)) ? id : -1;
                }
            }
        }

        public UserProfile GetUserProfileByFirebaseUserId(string firebaseUserId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [Username], [Password], [CreateTime] FROM [User] WHERE [FirebaseUserId] = @FirebaseUserId";
                    command.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                Username = usernameValue,
                                Password = passwordValue,
                                CreateTime = createTimeValue
                            };

                            return userProfile;
                        }
                        return null; // Return null if no user with the given FirebaseUserId is found.
                    }
                }
            }
        }
    }
}
