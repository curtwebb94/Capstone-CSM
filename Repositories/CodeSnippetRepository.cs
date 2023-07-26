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
                    command.CommandText = "SELECT [Id], [UserId], [Title], [Content], [CreateTime], [CreatedBy] FROM [CodeSnippet]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<CodeSnippet> codeSnippets = new List<CodeSnippet>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");
                            int createdByColumnPosition = reader.GetOrdinal("CreatedBy");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string titleValue = reader.GetString(titleColumnPosition);
                            string contentValue = reader.GetString(contentColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);
                            string createdByValue = reader.GetString(createdByColumnPosition);

                            CodeSnippet codeSnippet = new CodeSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                Title = titleValue,
                                Content = contentValue,
                                CreateTime = createTimeValue,
                                CreatedBy = createdByValue
                            };

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
                    command.CommandText = "SELECT [Id], [UserId], [Title], [Content], [CreateTime], [CreatedBy] FROM [CodeSnippet] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int userIdColumnPosition = reader.GetOrdinal("UserId");
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            int createTimeColumnPosition = reader.GetOrdinal("CreateTime");
                            int createdByColumnPosition = reader.GetOrdinal("CreatedBy");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int userIdValue = reader.GetInt32(userIdColumnPosition);
                            string titleValue = reader.GetString(titleColumnPosition);
                            string contentValue = reader.GetString(contentColumnPosition);
                            DateTime createTimeValue = reader.GetDateTime(createTimeColumnPosition);
                            string createdByValue = reader.GetString(createdByColumnPosition);

                            CodeSnippet codeSnippet = new CodeSnippet
                            {
                                Id = idValue,
                                UserId = userIdValue,
                                Title = titleValue,
                                Content = contentValue,
                                CreateTime = createTimeValue,
                                CreatedBy = createdByValue
                            };

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
                        INSERT INTO [CodeSnippet] ([UserId], [Title], [Content], [CreateTime], [CreatedBy])
                        VALUES (@UserId, @Title, @Content, @CreateTime, @CreatedBy)
                    ";

                    command.Parameters.AddWithValue("@UserId", codeSnippet.UserId);
                    command.Parameters.AddWithValue("@Title", codeSnippet.Title);
                    command.Parameters.AddWithValue("@Content", codeSnippet.Content);
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
                            [CreateTime] = @CreateTime,
                            [CreatedBy] = @CreatedBy
                        WHERE [Id] = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", codeSnippet.Id);
                    command.Parameters.AddWithValue("@UserId", codeSnippet.UserId);
                    command.Parameters.AddWithValue("@Title", codeSnippet.Title);
                    command.Parameters.AddWithValue("@Content", codeSnippet.Content);
                    command.Parameters.AddWithValue("@CreateTime", codeSnippet.CreateTime);
                    command.Parameters.AddWithValue("@CreatedBy", codeSnippet.CreatedBy);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
