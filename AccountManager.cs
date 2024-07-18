using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApp
{
    public static class AccountManager
    {
        static Human? _human;

        public static bool IsLoggedIn => _human != null;

        public static bool Login(int id, string password)
        {
            if (IsLoggedIn) return false;

            var human = Library.HumanRepo.MyList.FirstOrDefault(a => a.Id == id);

            if(human == null) return false;

            if(human.Password != password) return false;

            _human = human;
            return true;
        }

        public static void Logout()
        {
            if (!IsLoggedIn) return;

            _human = null;
        }

        public static bool Register(Human human)
        {
            if (IsLoggedIn) return false;
            var humans = Library.HumanRepo.MyList;
            if (humans.Any(m => m.Id == human.Id)) return false;
            humans.Add((Member)human);
            Login(human.Id, human.Password);
            return true;
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