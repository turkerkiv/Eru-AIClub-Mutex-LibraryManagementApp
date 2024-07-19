using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApp.View;

public class UIHandler
{
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
                if (AccountManager.CurrentHuman is Member member)
                {
                    System.Console.WriteLine("1- Search book by name");
                    System.Console.WriteLine("2- Borrow book by Id");
                }
                if (AccountManager.CurrentHuman is Author author)
                {
                    System.Console.WriteLine("3- Write a page");
                    System.Console.WriteLine("4- Turn all pages into book");
                }
                if (AccountManager.CurrentHuman is Recepcionist recepcionist)
                {
                    System.Console.WriteLine("1- Register a member");
                    System.Console.WriteLine("2- See pending borrow request");
                    System.Console.WriteLine("3- See pending book creation request");
                }
                if (AccountManager.CurrentHuman is Manager manager)
                {
                    System.Console.WriteLine("1- Change role of someone");
                }

                System.Console.WriteLine("9- Read a book");
                System.Console.WriteLine("0- Logout");
            }
            else
            {
                System.Console.WriteLine("1- Login");
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
            switch (AccountManager.CurrentHuman)
            {
                case Author author:
                    switch (inputKey)
                    {
                        case ConsoleKey.D3:
                            WritePageUI(author);
                            break;
                        case ConsoleKey.D4:
                            CreateBookUI(author);
                            break;
                    }
                    break;
                case Member member:
                    switch (inputKey)
                    {
                        case ConsoleKey.D1:
                            //search book
                            break;
                        case ConsoleKey.D2:
                            //borrow book
                            break;
                    }
                    break;
                case Recepcionist recepcionist:
                    switch (inputKey)
                    {
                        case ConsoleKey.D1:
                            RegisterUI(recepcionist);
                            break;
                        case ConsoleKey.D2:
                            //approve borrow request with 1 more click 
                            break;
                        case ConsoleKey.D3:
                            //read book then approve with 1 more click
                            break;
                    }
                    break;
                case Manager manager:
                    switch (inputKey)
                    {
                        case ConsoleKey.D1:
                            //change role with generic method
                            break;
                    }
                    break;

            }
            switch (inputKey)
            {
                case ConsoleKey.D9:
                //read book
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

    private void RegisterUI(Recepcionist rece)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter member's name: ");
        string name = Console.ReadLine() ?? "";
        System.Console.WriteLine("Enter member's surname: ");
        string surname = Console.ReadLine() ?? "";
        System.Console.WriteLine("Enter member's age: ");
        int age = UIHelper.GetValidInteger();

        Member? member = new Member("", name, surname, age);
        string temppass = rece.SignUpMember(member);

        System.Console.WriteLine("Signed up. Here is the temporary password of member: " + temppass);
    }

    private void WritePageUI(Author author)
    {
        System.Console.WriteLine("You can start writing now.");
        string text = Console.ReadLine() ?? "";
        while (!author.TryWritePage(text))
        {
            System.Console.WriteLine("Please don't exceed 200 words.");
            text = Console.ReadLine() ?? "";
        }
    }

    private void CreateBookUI(Author author)
    {
        System.Console.WriteLine("Enter book's name: ");
        string name = Console.ReadLine() ?? "";
        author.CreateBook(name);
    }
}