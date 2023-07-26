using System.ComponentModel.DataAnnotations;

namespace CSM.Models
{
    public class CodeSnippetTag
    {
        public int Id { get; set; }

        [Required]
        public int SnippetId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
