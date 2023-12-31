﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSM.Models
{
    public class CodeSnippet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }
        public List<string> Tags { get; set; } // New property for tags

    }
}
