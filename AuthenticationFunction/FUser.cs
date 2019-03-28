using AuthenticationModel;
using System;

namespace AuthenticationFunction
{
    public class FUser: IFUser
    {
        public bool Login(User user)
        {
            if (user == null)
                return false;

            return user.UserName != "username" && user.Password != "password"; //ToDo: connect this to a database
        }
    }
}
