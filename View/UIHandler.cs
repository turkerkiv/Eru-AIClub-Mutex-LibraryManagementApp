using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApp.View;

public class UIHandler
{
    public UIHandler()
    {
    }

    public void RunTheApp()
    {
        System.Console.WriteLine("Welcome to the library!");

        ConsoleKey inputKey = ConsoleKey.None;
        while (inputKey != ConsoleKey.Escape)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Here is the list of operations you can do: ");
            System.Console.WriteLine();

            if (AccountManager.IsLoggedIn)
            {
                System.Console.WriteLine("0- Logout");
            }
            else
            {
                System.Console.WriteLine("1- Login");
                System.Console.WriteLine("2- Register");
                System.Console.WriteLine("ESC- Exit");
            }

            inputKey = Console.ReadKey().Key;
            ProcessInputKey(inputKey);
        }
    }

    private void ProcessInputKey(ConsoleKey inputKey)
    {
        if (AccountManager.IsLoggedIn)
        {
            switch (inputKey)
            {
                case ConsoleKey.D0:
                    AccountManager.Logout();
                    break;
            }
        }
        else
        {
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    LoginUI();
                    break;
                case ConsoleKey.D2:
                    RegisterUI();
                    break;
            }
        }
    }

    private void LoginUI()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter your ID: ");
        int id = UIHelper.GetValidInteger();
        System.Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine() ?? "";
        AccountManager.Login(id, password);
    }

    private void RegisterUI()
    {
        //maybe change here to be id name password etc again and then make it smt
        System.Console.WriteLine();
        System.Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine() ?? "";
        System.Console.WriteLine("Enter your surname: ");
        string surname = Console.ReadLine() ?? "";
        System.Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine() ?? "";
        System.Console.WriteLine("Enter your age: ");
        int age = UIHelper.GetValidInteger();

        Member? member = new Member(password, name, surname, age);

        AccountManager.Register(member);
    }
}