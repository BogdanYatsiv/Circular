using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Circular.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; }

        //public string Password { get; set; }

        public string Email { get; set; }
    }

    public class ProfileEditModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


    }

    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }


}
