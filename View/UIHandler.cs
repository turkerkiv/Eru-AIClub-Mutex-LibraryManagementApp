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
                }
                if (AccountManager.CurrentHuman is Manager manager)
                {
                    System.Console.WriteLine("1- Change role of someone");
                    System.Console.WriteLine("2- See pending book creation request");
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
                            SearchBookUI(member);
                            break;
                        case ConsoleKey.D2:
                            BorrowBookUI(member);
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
                            SeeBorrowRequestsUI(recepcionist);
                            break;
                    }
                    break;
                case Manager manager:
                    switch (inputKey)
                    {
                        case ConsoleKey.D1:
                            //change role with generic method
                            break;
                        case ConsoleKey.D2:
                            ReadBookUI(manager);
                            break;
                    }
                    break;

            }
            switch (inputKey)
            {
                case ConsoleKey.D9:
                    ReadBookUI(AccountManager.CurrentHuman!);
                    break;
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

    private void SearchBookUI(Human human)
    {
        System.Console.WriteLine("Search book by name: ");
        string input = Console.ReadLine() ?? "";
        List<Book> books = human.SearchBook(input).GetRange(0, 5);
        System.Console.WriteLine("Here is the first 5 book that matches your input: ");
        foreach (var b in books)
        {
            System.Console.WriteLine($"Book Id: {b.Id}, Book name: {b.Name}, Author: {String.Join(' ', b.Authors)}, Page: {b.PageCount} Available to borrow?: {b.IsAvailable} " + (b.IsAvailable ? "" : "Date left to be available: " + b.DateLeft));
        }
    }

    private void BorrowBookUI(Member member)
    {
        System.Console.WriteLine("Enter the id of book you wanna borrow: ");
        int id = UIHelper.GetValidIntWithinRange(Library.BookRepo.MyList.Count + 1);
        bool result = member.TryBorrowBook(id);
        if (result)
            System.Console.WriteLine("Request arrived. Wait for the recepcionist's approval.");
        else
            System.Console.WriteLine("You tried to borrow book that already borrowed. Please wait");
    }

    //for members to read
    private void ReadBookUI(Human human)
    {
        System.Console.WriteLine("Enter book's Id: ");
        int id = UIHelper.GetValidIntWithinRange(Library.BookRepo.MyList.Count + 1);

        Book book = human.ReadBook(id);

        UIHelper.ReadBook(book);
    }

    //for managers to check new book
    private void ReadBookUI(Manager manager)
    {
        Book book = manager.PendingBooksToCreate.Peek();

        UIHelper.ReadBook(book);

        System.Console.WriteLine($"Other infos about book: Name: {book.Name}, Author: {String.Join(' ', book.Authors)}");
        System.Console.WriteLine("Do you want to add this book to library? (y/n)");
        var key = Console.ReadKey().Key;
        if(key == ConsoleKey.Y)
        {
            System.Console.WriteLine("Book added!");
            manager.AddBookToLibrary();
        }
        else
        {
            System.Console.WriteLine("Book removed!");
            manager.PendingBooksToCreate.Dequeue();
        }
    }

    private void SeeBorrowRequestsUI(Recepcionist recepcionist)
    {
        BookRequest bookReq = recepcionist.BookBorrowRequests.Peek();
        Book book = Library.BookRepo.MyList.Find(b => b.Id == bookReq.BookId)!;
        Member member = (Member)Library.HumanRepo.MyList.Find(h => h.Id == bookReq.HumanId)!;
        System.Console.WriteLine($"Member named {member.Name} is want to borrow the book called {book.Name}. Do you want to accept? (y/n)");
        var key = Console.ReadKey().Key;
        if (key == ConsoleKey.Y)
        {
            System.Console.WriteLine("Request approved");
            recepcionist.LendBookFor14Days();
        }
        else if (key == ConsoleKey.N)
        {
            System.Console.WriteLine("Request removed");
            recepcionist.BookBorrowRequests.Dequeue();
        }
    }

}