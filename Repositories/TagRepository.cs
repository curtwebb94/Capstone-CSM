using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CSM.Models;

namespace CSM.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }

        public List<Tag> GetAllTags()
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [Name], [Category] FROM [Tag]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Tag> tags = new List<Tag>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int nameColumnPosition = reader.GetOrdinal("Name");
                            int categoryColumnPosition = reader.GetOrdinal("Category");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string nameValue = reader.GetString(nameColumnPosition);
                            string categoryValue = reader.GetString(categoryColumnPosition);

                            Tag tag = new Tag
                            {
                                Id = idValue,
                                Name = nameValue,
                                Category = categoryValue
                            };

                            tags.Add(tag);
                        }
                        return tags;
                    }
                }
            }
        }

        public Tag GetTagById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [Name], [Category] FROM [Tag] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int nameColumnPosition = reader.GetOrdinal("Name");
                            int categoryColumnPosition = reader.GetOrdinal("Category");

                            int idValue = reader.GetInt32(idColumnPosition);
                            string nameValue = reader.GetString(nameColumnPosition);
                            string categoryValue = reader.GetString(categoryColumnPosition);

                            Tag tag = new Tag
                            {
                                Id = idValue,
                                Name = nameValue,
                                Category = categoryValue
                            };

                            return tag;
                        }
                        return null; // Return null if no tag with the given id is found.
                    }
                }
            }
        }

        public void AddTag(Tag tag)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Tag] ([Name], [Category]) VALUES (@Name, @Category)";
                    command.Parameters.AddWithValue("@Name", tag.Name);
                    command.Parameters.AddWithValue("@Category", tag.Category);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTag(Tag tag)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Tag] SET [Name] = @Name, [Category] = @Category WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", tag.Id);
                    command.Parameters.AddWithValue("@Name", tag.Name);
                    command.Parameters.AddWithValue("@Category", tag.Category);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Tag] WHERE [Id] = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
