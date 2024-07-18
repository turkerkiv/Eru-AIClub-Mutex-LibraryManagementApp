using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace
{
    public class AccountManager
    {
        Human _human;

        public bool IsLoggedIn => _human != null;

        public bool Login(Human human, string password)
        {
            if (_isLoggedIn) return false;

            //check the passwords etc.
            _human = human;
            return true;
        }

        public void Logout()
        {
            if (!_isLoggedIn) return;

            _human = null;
        }

        public void Register(Human human, string password)
        {
            if(IsLoggedIn) return;
            //check if there is same user id and then register it
            Login(human, password);
        }

        public bool ChangePassword(string newPassword)
        {
            if(!IsLoggedIn) return false;

            //check _human first
            _human.Password = newPassword;
            //save to the db.
            return newPassword;   
        }
    }
}