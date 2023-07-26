using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;
using CSM.Repositories;

namespace CSM.Repositories
{
    public class CodeSnippetTagRepository : BaseRepository, ICodeSnippetTagRepository
    {
        public CodeSnippetTagRepository(IConfiguration configuration) : base(configuration) { }

        public List<CodeSnippetTag> GetAllCodeSnippetTags()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [SnippetId], [TagId] FROM [CodeSnippetTag]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<CodeSnippetTag> codeSnippetTags = new List<CodeSnippetTag>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int tagIdColumnPosition = reader.GetOrdinal("TagId");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            int tagIdValue = reader.GetInt32(tagIdColumnPosition);

                            CodeSnippetTag codeSnippetTag = new CodeSnippetTag
                            {
                                Id = idValue,
                                SnippetId = snippetIdValue,
                                TagId = tagIdValue
                            };

                            codeSnippetTags.Add(codeSnippetTag);
                        }

                        return codeSnippetTags;
                    }
                }
            }
        }

        public CodeSnippetTag GetCodeSnippetTagById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [SnippetId], [TagId] FROM [CodeSnippetTag] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int snippetIdColumnPosition = reader.GetOrdinal("SnippetId");
                            int tagIdColumnPosition = reader.GetOrdinal("TagId");

                            int idValue = reader.GetInt32(idColumnPosition);
                            int snippetIdValue = reader.GetInt32(snippetIdColumnPosition);
                            int tagIdValue = reader.GetInt32(tagIdColumnPosition);

                            CodeSnippetTag codeSnippetTag = new CodeSnippetTag
                            {
                                Id = idValue,
                                SnippetId = snippetIdValue,
                                TagId = tagIdValue
                            };

                            return codeSnippetTag;
                        }

                        return null; // Return null if no CodeSnippetTag with the given id is found.
                    }
                }
            }
        }

        public void AddCodeSnippetTag(CodeSnippetTag codeSnippetTag)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO [CodeSnippetTag] ([SnippetId], [TagId])
                        VALUES (@SnippetId, @TagId)
                    ";
                    command.Parameters.AddWithValue("@SnippetId", codeSnippetTag.SnippetId);
                    command.Parameters.AddWithValue("@TagId", codeSnippetTag.TagId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCodeSnippetTag(CodeSnippetTag codeSnippetTag)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE [CodeSnippetTag]
                        SET [SnippetId] = @SnippetId,
                            [TagId] = @TagId
                        WHERE [Id] = @Id
                    ";
                    command.Parameters.AddWithValue("@SnippetId", codeSnippetTag.SnippetId);
                    command.Parameters.AddWithValue("@TagId", codeSnippetTag.TagId);
                    command.Parameters.AddWithValue("@Id", codeSnippetTag.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCodeSnippetTag(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [CodeSnippetTag] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
