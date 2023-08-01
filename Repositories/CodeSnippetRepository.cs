using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;

namespace CSM.Repositories
{
    public class CodeSnippetRepository : BaseRepository, ICodeSnippetRepository
    {
        public CodeSnippetRepository(IConfiguration configuration) : base(configuration) { }

        public List<CodeSnippet> GetAllCodeSnippets()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [Title], [Content], [Description], [CreateTime], [CreatedBy] FROM [CodeSnippet]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<CodeSnippet> codeSnippets = new List<CodeSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            int descriptionColumnPosition = reader.GetOrdinal("Description");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");
                            int createdByColumnPosition = reader.GetOrdinal("CreatedBy");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string titleValue = reader.GetString(titleColumnPosition);
                            string contentValue = reader.GetString(contentColumnPosition);
                            string descriptionValue = reader.GetString(descriptionColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);
                            string createdByValue = reader.GetString(createdByColumnPosition);

                            CodeSnippet codeSnippet = new CodeSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                Title = titleValue,
                                Content = contentValue,
                                Description = descriptionValue,
                                CreateTime = createTimeValue,
                                CreatedBy = createdByValue
                            };

                            // Fetch tags for the code snippet using CodeSnippetTag table
                            List<string> tags = GetTagsForCodeSnippet(idValue);
                            codeSnippet.Tags = tags;

                            codeSnippets.Add(codeSnippet);
                        }
                        return codeSnippets;
                    }
                }
            }
        }

        public CodeSnippet GetCodeSnippetById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [Title], [Content], [Description], [CreateTime], [CreatedBy] FROM [CodeSnippet] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            int descriptionColumnPosition = reader.GetOrdinal("Description");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");
                            int createdByColumnPosition = reader.GetOrdinal("CreatedBy");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string titleValue = reader.GetString(titleColumnPosition);
                            string contentValue = reader.GetString(contentColumnPosition);
                            string descriptionValue = reader.GetString(descriptionColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);
                            string createdByValue = reader.GetString(createdByColumnPosition);

                            CodeSnippet codeSnippet = new CodeSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                Title = titleValue,
                                Content = contentValue,
                                Description = descriptionValue,
                                CreateTime = createTimeValue,
                                CreatedBy = createdByValue
                            };

                            // Fetch tags for the code snippet using CodeSnippetTag table
                            List<string> tags = GetTagsForCodeSnippet(idValue);
                            codeSnippet.Tags = tags;

                            return codeSnippet;
                        }
                        return null; // Return null if no code snippet with the given id is found.
                    }
                }
            }
        }

        public void AddCodeSnippet(CodeSnippet codeSnippet)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO [CodeSnippet] ([UserId], [Title], [Content], [Description], [CreateTime], [CreatedBy])
                        VALUES (@UserId, @Title, @Content, @Description, @CreateTime, @CreatedBy)
                    ";

                    command.Parameters.AddWithValue("@UserId", codeSnippet.UserId);
                    command.Parameters.AddWithValue("@Title", codeSnippet.Title);
                    command.Parameters.AddWithValue("@Content", codeSnippet.Content);
                    command.Parameters.AddWithValue("@Description", codeSnippet.Description);
                    command.Parameters.AddWithValue("@CreateTime", codeSnippet.CreateTime);
                    command.Parameters.AddWithValue("@CreatedBy", codeSnippet.CreatedBy);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCodeSnippet(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [CodeSnippet] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditCodeSnippet(CodeSnippet codeSnippet)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE [CodeSnippet]
                        SET [UserId] = @UserId,
                            [Title] = @Title,
                            [Content] = @Content,
                            [Description] = @Description,
                            [CreateTime] = @CreateTime,
                            [CreatedBy] = @CreatedBy
                        WHERE [Id] = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", codeSnippet.Id);
                    command.Parameters.AddWithValue("@UserId", codeSnippet.UserId);
                    command.Parameters.AddWithValue("@Title", codeSnippet.Title);
                    command.Parameters.AddWithValue("@Content", codeSnippet.Content);
                    command.Parameters.AddWithValue("@Description", codeSnippet.Description);
                    command.Parameters.AddWithValue("@CreateTime", codeSnippet.CreateTime);
                    command.Parameters.AddWithValue("@CreatedBy", codeSnippet.CreatedBy);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<CodeSnippet> GetCodeSnippetsByUserId(int userId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [UserId], [Title], [Content], [Description], [CreateTime], [CreatedBy] FROM [CodeSnippet] WHERE [UserId] = @UserId";
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<CodeSnippet> codeSnippets = new List<CodeSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            int descriptionColumnPosition = reader.GetOrdinal("Description");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");
                            int createdByColumnPosition = reader.GetOrdinal("CreatedBy");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string titleValue = reader.GetString(titleColumnPosition);
                            string contentValue = reader.GetString(contentColumnPosition);
                            string descriptionValue = reader.GetString(descriptionColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);
                            string createdByValue = reader.GetString(createdByColumnPosition);

                            CodeSnippet codeSnippet = new CodeSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                Title = titleValue,
                                Content = contentValue,
                                Description = descriptionValue,
                                CreateTime = createTimeValue,
                                CreatedBy = createdByValue
                            };

                            // Fetch tags for the code snippet using CodeSnippetTag table
                            List<string> tags = GetTagsForCodeSnippet(idValue);
                            codeSnippet.Tags = tags;

                            codeSnippets.Add(codeSnippet);
                        }

                        return codeSnippets;
                    }
                }
            }
        }

        private List<string> GetTagsForCodeSnippet(int codeSnippetId)
        {
            List<string> tags = new List<string>();

            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT t.[Name]
                        FROM [CodeSnippetTag] cst
                        JOIN [Tag] t ON cst.TagId = t.Id
                        WHERE cst.SnippetId = @SnippetId
                    ";
                    command.Parameters.AddWithValue("@SnippetId", codeSnippetId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tagName = reader.GetString(0);
                            tags.Add(tagName);
                        }
                    }
                }
            }

            return tags;
        }
    }
}
