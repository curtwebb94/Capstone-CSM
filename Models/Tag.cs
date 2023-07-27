﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CSM.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
