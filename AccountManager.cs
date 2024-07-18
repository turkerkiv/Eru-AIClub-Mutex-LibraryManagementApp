using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApp
{
    public static class AccountManager
    {
        static Human? _human;

        //şu an tüm classlar buna erişebilir ve bu demek oluyor ki bir member bile üye kaydedebilir???
        public static bool IsLoggedIn => _human != null;

        public static bool Login(int id, string password)
        {
            //check id and create object according to that id. if it is author then author if it is member then member
            if (IsLoggedIn) return false;

            //check the passwords etc.
            // _human = human;
            return true;
        }

        public static void Logout()
        {
            if (!IsLoggedIn) return;

            _human = null;
        }

        public static void Register(Human human)
        {
            if (IsLoggedIn) return;
            //check if there is same user id and then register it
            Login(human.Id, human.Password);
        }

        public static bool ChangePassword(string newPassword)
        {
            if (!IsLoggedIn) return false;

            //check _human first
            // _human.Password = newPassword;
            //save to the db.
            return true;
        }
    }
}