using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Models
{
    public class Appuser: IdentityUser
    {
        [StringLength(maximumLength:50)]
        public string Fullname { get; set; }
    }
}
