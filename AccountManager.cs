using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace
{
    public class AccountManager
    {
        Human _human;
        bool _isLoggedIn = _human != null;

        public void Login(Human human)
        {
            if (_isLoggedIn) return;

            //check the passwords etc.
            _human = human;
        }

        public void Logout()
        {
            if (!_isLoggedIn) return;

            _human = null;
        }
    }
}