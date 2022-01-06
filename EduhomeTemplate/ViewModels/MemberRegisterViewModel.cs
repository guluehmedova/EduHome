using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.ViewModels
{
    public class MemberRegisterViewModel
    {
        [StringLength(maximumLength:20)]
        public string Username { get; set; }
        [StringLength(maximumLength:25, MinimumLength =8)]
        public string Password { get; set; }
        [StringLength(maximumLength:100)]
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
