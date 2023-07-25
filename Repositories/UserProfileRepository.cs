using CSM.Repositories;
using Microsoft.Data.SqlClient;
using CSM.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;

namespace UserProfileRepository.Repositories
{
    /// <summary>
    ///  This class is responsible for interacting with Room data.
    ///  It inherits from the BaseRepository class so that it can use the BaseRepository's Connection property
    /// </summary>
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        /// <summary>
        ///  When new RoomRepository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }


        public List<UserProfile> GetAllUserProfiles()
        {
            using (SqlConnection connection = Connection)
            {
                // Note, we must Open() the connection, the "using" block doesn't do that for us.
                connection.Open();

                // We must "use" commands too.
                using (SqlCommand command = connection.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    command.CommandText = "SELECT [Id], [Username], [Email], [Password], [CreateTime] FROM [User]";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // A list to hold the user profiles we retrieve from the database.
                        List<UserProfile> userProfiles = new List<UserProfile>();

                        // Read() will return true if there's more data to read
                        while (reader.Read())
                        {
                            // The "ordinal" is the numeric position of the column in the query results.
                            // For our query, "Id" has an ordinal value of 0, "Username" is 1, and so on.
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int usernameColumnPosition = reader.GetOrdinal("Username");
                            int emailColumnPosition = reader.GetOrdinal("Email");
                            int passwordColumnPosition = reader.GetOrdinal("Password");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            // We use the reader's GetXXX methods to get the value for a particular ordinal.
                            int idValue = reader.GetInt32(idColumnPosition);
                            string usernameValue = reader.GetString(usernameColumnPosition);
                            string emailValue = reader.GetString(emailColumnPosition);
                            string passwordValue = reader.GetString(passwordColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            // Now let's create a new user profile object using the data from the database.
                            UserProfile userProfile = new UserProfile
                            {
                                Id = idValue,
                                Username = usernameValue,
                                Email = emailValue,
                                Password = passwordValue,
                                CreateTime = createTimeValue
                            };

                            // ...and add that user profile object to our list.
                            userProfiles.Add(userProfile);
                        }
                        // Return the list of user profiles to whomever called this method.
                        return userProfiles;
                    }
                }
            }
        }
    }
}