using AuthenticationModel;
using System;

namespace AuthenticationFunction
{
    public class FUser: IFUser
    {
        public User Login(User user)
        {
            if (user == null || user.UserName != "username" || user.Password != "password")
                return null;

            //ToDo: connect this to a database
            return new User
            {
                UserId = 1,
                UserName = "username"
            };
        }
    }
}
