using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;

namespace CSM.Repositories
{
    public class UserPreferenceRepository : BaseRepository, IUserPreferenceRepository
    {
        public UserPreferenceRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserPreference> GetAllUserPreferences()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [BackgroundColor], [FontColor], [FontSize] FROM [UserPreference]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<UserPreference> userPreferences = new List<UserPreference>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int backgroundColorColumnPosition = reader.GetOrdinal("BackgroundColor");
                            int fontColorColumnPosition = reader.GetOrdinal("FontColor");
                            int fontSizeColumnPosition = reader.GetOrdinal("FontSize");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string backgroundColorValue = reader.GetString(backgroundColorColumnPosition);
                            string fontColorValue = reader.GetString(fontColorColumnPosition);
                            int fontSizeValue = reader.GetInt32(fontSizeColumnPosition);

                            UserPreference userPreference = new UserPreference
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                BackgroundColor = backgroundColorValue,
                                FontColor = fontColorValue,
                                FontSize = fontSizeValue
                            };

                            userPreferences.Add(userPreference);
                        }

                        return userPreferences;
                    }
                }
            }
        }

        public UserPreference GetByUserId(int userId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [BackgroundColor], [FontColor], [FontSize] FROM [UserPreference] WHERE [UserId] = @UserId";
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int backgroundColorColumnPosition = reader.GetOrdinal("BackgroundColor");
                            int fontColorColumnPosition = reader.GetOrdinal("FontColor");
                            int fontSizeColumnPosition = reader.GetOrdinal("FontSize");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string backgroundColorValue = reader.GetString(backgroundColorColumnPosition);
                            string fontColorValue = reader.GetString(fontColorColumnPosition);
                            int fontSizeValue = reader.GetInt32(fontSizeColumnPosition);

                            UserPreference userPreference = new UserPreference
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                BackgroundColor = backgroundColorValue,
                                FontColor = fontColorValue,
                                FontSize = fontSizeValue
                            };

                            return userPreference;
                        }

                        return null; // Return null if no user preference with the given userId is found.
                    }
                }
            }
        }

        public void AddUserPreference(UserPreference userPreference)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [UserPreference] ([UserId], [BackgroundColor], [FontColor], [FontSize]) " +
                                          "VALUES (@UserId, @BackgroundColor, @FontColor, @FontSize)";
                    command.Parameters.AddWithValue("@UserId", userPreference.UserId);
                    command.Parameters.AddWithValue("@BackgroundColor", userPreference.BackgroundColor);
                    command.Parameters.AddWithValue("@FontColor", userPreference.FontColor);
                    command.Parameters.AddWithValue("@FontSize", userPreference.FontSize);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUserPreference(UserPreference userPreference)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [UserPreference] " +
                                          "SET [BackgroundColor] = @BackgroundColor, [FontColor] = @FontColor, [FontSize] = @FontSize " +
                                          "WHERE [UserId] = @UserId";
                    command.Parameters.AddWithValue("@UserId", userPreference.UserId);
                    command.Parameters.AddWithValue("@BackgroundColor", userPreference.BackgroundColor);
                    command.Parameters.AddWithValue("@FontColor", userPreference.FontColor);
                    command.Parameters.AddWithValue("@FontSize", userPreference.FontSize);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserPreference(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [UserPreference] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
