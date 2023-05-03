using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class LoginAdmin
    {
        [Required(ErrorMessage = "Phone is Required")]
        [MinLength(11 ,ErrorMessage ="Phone Number Must Contain 11 Number")]
        [MaxLength(11, ErrorMessage = "Phone Number Must Contain 11 Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        public bool saveMe { get; set; } = false;

        public string ErrorMsg { get; set; }

    }
}