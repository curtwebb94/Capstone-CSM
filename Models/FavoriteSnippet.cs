using System;


namespace CSM.Models
{
    public class FavoriteSnippet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SnippetId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
