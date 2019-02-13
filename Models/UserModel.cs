using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MigrationPortal.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "User Company")]
        public string UserCompany { get; set; }

        [Required]
        [Display(Name = "User Department")]
        public string UserDept { get; set; }

        [Required]
        [Display(Name = "Computer Service Tag")]
        public string Hostname { get; set; }

        [Required]
        [Display(Name = "Accept Terms & Conditions")]


        public bool AcceptTC { get; set; }

        public List<Adapter> NetworkAdapters { get; set; }

    }
}