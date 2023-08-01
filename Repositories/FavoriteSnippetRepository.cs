using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;

namespace CSM.Repositories
{
    public class FavoriteSnippetRepository : BaseRepository, IFavoriteSnippetRepository
    {
        public FavoriteSnippetRepository(IConfiguration configuration) : base(configuration) { }

        public List<FavoriteSnippet> GetAllFavoriteSnippets()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [SnippetId], [CreateTime] FROM [FavoriteSnippet]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<FavoriteSnippet> favoriteSnippets = new List<FavoriteSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            FavoriteSnippet favoriteSnippet = new FavoriteSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                SnippetId = snippetIdValue,
                                CreateTime = createTimeValue
                            };

                            favoriteSnippets.Add(favoriteSnippet);
                        }

                        return favoriteSnippets;
                    }
                }
            }
        }

        public FavoriteSnippet GetFavoriteSnippetById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [SnippetId], [CreateTime] FROM [FavoriteSnippet] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            FavoriteSnippet favoriteSnippet = new FavoriteSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                SnippetId = snippetIdValue,
                                CreateTime = createTimeValue
                            };

                            return favoriteSnippet;
                        }

                        return null;
                    }
                }
            }
        }

        public List<FavoriteSnippet> GetFavoriteSnippetsByUserId(int userId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [SnippetId], [CreateTime] FROM [FavoriteSnippet] WHERE [UserId] = @UserId";
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<FavoriteSnippet> favoriteSnippets = new List<FavoriteSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            FavoriteSnippet favoriteSnippet = new FavoriteSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                SnippetId = snippetIdValue,
                                CreateTime = createTimeValue
                            };

                            favoriteSnippets.Add(favoriteSnippet);
                        }

                        return favoriteSnippets;
                    }
                }
            }
        }

        public List<FavoriteSnippet> GetFavoriteSnippetsByFirebaseId(string firebaseId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT fs.[Id], fs.[UserId], fs.[SnippetId], fs.[CreateTime]
                                   FROM [FavoriteSnippet] fs
                                   JOIN [User] u ON fs.[UserId] = u.[Id]
                                   WHERE u.[FirebaseId] = @FirebaseId";
                    command.Parameters.AddWithValue("@FirebaseId", firebaseId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<FavoriteSnippet> favoriteSnippets = new List<FavoriteSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);

                            FavoriteSnippet favoriteSnippet = new FavoriteSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                SnippetId = snippetIdValue,
                                CreateTime = createTimeValue
                            };

                            favoriteSnippets.Add(favoriteSnippet);
                        }

                        return favoriteSnippets;
                    }
                }
            }
        }


        public void AddFavoriteSnippet(FavoriteSnippet favoriteSnippet)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [FavoriteSnippet] ([UserId], [SnippetId], [CreateTime])
                                            VALUES (@UserId, @SnippetId, @CreateTime)";
                    command.Parameters.AddWithValue("@UserId", favoriteSnippet.UserId);
                    command.Parameters.AddWithValue("@SnippetId", favoriteSnippet.SnippetId);
                    command.Parameters.AddWithValue("@CreateTime", favoriteSnippet.CreateTime);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFavoriteSnippet(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [FavoriteSnippet] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFavoriteSnippet(FavoriteSnippet favoriteSnippet)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [FavoriteSnippet]
                                            SET [UserId] = @UserId,
                                                [SnippetId] = @SnippetId,
                                                [CreateTime] = @CreateTime
                                            WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@UserId", favoriteSnippet.UserId);
                    command.Parameters.AddWithValue("@SnippetId", favoriteSnippet.SnippetId);
                    command.Parameters.AddWithValue("@CreateTime", favoriteSnippet.CreateTime);
                    command.Parameters.AddWithValue("@Id", favoriteSnippet.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
