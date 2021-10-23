using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Entities.Users
{
    public partial class User
    {
        public User(string userName
            , string password
            ) : base()
        {
            UserName = userName;
            Password = password;
        }


        public bool ValidOnAdd()
        {
            return
                !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(Password);
        }
    }
}
