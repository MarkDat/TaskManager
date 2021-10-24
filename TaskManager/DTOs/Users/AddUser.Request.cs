using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Users
{
    public class AddUserRequest
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        public string Image { get; set; }
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
