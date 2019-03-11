﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class CourseRegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int PublisherId { get; set; }
    }
}
