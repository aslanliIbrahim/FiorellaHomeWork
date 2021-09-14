using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Areas.AdminPanel.ViewModels
{
    public class AdminRegisterVM
    {
        [Required, StringLength(maximumLength:20)]
        public string Username { get; set; }
        
        public string  Email { get; set; }
        public List<RoleVM> Roles { get; set; }
        public string Role { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
