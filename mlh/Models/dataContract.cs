﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace mlh.Models
{
    public class CreateUser
    {
        [Required,MinLength(6),MaxLength(30)]
        public string username { get; set; }
        [Required, MinLength(6), MaxLength(30)]
        public string password { get; set; }
    }
    public class availability:CreateUser{
        public string schedule { get; set; }
    }
    public class assignment:CreateUser{
        [Required]
        public Guid userid { get; set; }
        [Required]
        public Guid courseid { get; set; }
    }

    public class RequestTutor:CreateUser{
        public string tutorid { get; set; }
    }
    public class addcourse{
        public string name { get; set; }
        public string description { get; set; }
    }

    public class UserPreview{
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string availability { get; set; }
        public List<string> requests { get; set; }
        public List<string> services { get; set; }
        public List<KeyValuePair<string, string>> tutors { get; set; }
        public List<KeyValuePair<string, string>> tutees { get; set; }
    }
    public class CoursePreview{
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    
}
