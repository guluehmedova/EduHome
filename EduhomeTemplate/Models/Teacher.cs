using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profesion { get; set; }
        public string AboutTitle { get; set; }
        public string Desc { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Image { get; set; }
        [Required]
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
