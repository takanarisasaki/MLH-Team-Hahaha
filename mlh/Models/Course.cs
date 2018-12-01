using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace mlh.Models
{
    public class Course
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }



        public CoursePreview GetPreview(){
            return new CoursePreview
            {
                id = id,
                name = name,
                description = description
            };
        }
    }
}
