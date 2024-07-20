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
                //WHAT WE GONNA DO IF 2 PERSON WANTS SAME BOOK. LIKE SHOW IF BOOK IS AVAILABLE TO RECEPCIONIST
                //MAYBE MAKE MEMBERS CAN READ ONLY BOOKS THAT THEY BORROWED
                if (AccountManager.CurrentHuman is Member member)
                {
                    System.Console.WriteLine("1- Search book by name");//OK
                    System.Console.WriteLine("2- Borrow book by Id"); //OK
                }
                if (AccountManager.CurrentHuman is Author author)
                {
                    System.Console.WriteLine("3- Write a page"); //OK
                    System.Console.WriteLine("4- Turn all pages into book"); //OK
                }
                if (AccountManager.CurrentHuman is Recepcionist recepcionist)
                {
                    System.Console.WriteLine("1- Register a member"); //OK
                    System.Console.WriteLine("2- See pending borrow request"); //OK
                }
                if (AccountManager.CurrentHuman is Manager manager)
                {
                    System.Console.WriteLine("1- Change role of someone");
                    System.Console.WriteLine("2- See pending book creation request"); //OK
                }

                //add changing password
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
                        case ConsoleKey.D1:
                            SearchBookUI(author);
                            break;
                        case ConsoleKey.D2:
                            BorrowBookUI(author);
                            break;
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
        System.Console.WriteLine();
        System.Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine() ?? "";
        AccountManager.Login(id, password);
    }

    private void RegisterUI(Recepcionist rece)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter member's name: ");
        string name = Console.ReadLine() ?? "";
        System.Console.WriteLine();
        System.Console.WriteLine("Enter member's surname: ");
        string surname = Console.ReadLine() ?? "";
        System.Console.WriteLine();
        System.Console.WriteLine("Enter member's age: ");
        int age = UIHelper.GetValidInteger();

        Member? member = new Member("", name, surname, age);
        string temppass = rece.SignUpMember(member);

        System.Console.WriteLine();
        System.Console.WriteLine("!!!Signed up. Here is the temporary password of member: " + temppass + " !!!");
    }

    private void WritePageUI(Author author)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("!!!You can start writing now.!!!");
        string text = Console.ReadLine() ?? "";
        while (!author.TryWritePage(text))
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Please don't exceed 200 words!!!");
            text = Console.ReadLine() ?? "";
        }
    }

    private void CreateBookUI(Author author)
    {
        if (author.CurrentPages.Count == 0)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!You didn't write a single page!!!");
            return;
        }

        System.Console.WriteLine();
        System.Console.WriteLine("Enter book's name: ");
        string name = Console.ReadLine() ?? "";
        author.CreateBook(name);
    }

    private void SearchBookUI(Human human)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Search book by name: ");
        string input = Console.ReadLine() ?? "";
        List<Book> books = human.SearchBook(input);
        if (books == null || books.Count == 0)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!There is no book associated with your input!!!");
            return;
        }

        System.Console.WriteLine();
        System.Console.WriteLine("Here is the book/s that matches your input: (Max 5 book)");
        foreach (var b in books.GetRange(0, Math.Min(5, books.Count)))
        {
            System.Console.WriteLine("---------------------");
            System.Console.WriteLine($"Book Id: {b.Id}\nBook name: {b.Name}\nAuthor: {String.Join(' ', b.Authors.Select(a => a.Name))}\nPage: {b.PageCount}\nAvailable to borrow?: {b.IsAvailable}\n" + (b.IsAvailable ? "" : "Date left to be available: " + b.DateLeft.Days + " Days"));
        }
    }

    private void BorrowBookUI(Member member)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter the id of book you wanna borrow: ");
        int id = UIHelper.GetValidIntWithinRange(1, Library.BookRepo.MyList.Count + 1);
        bool result = member.TryBorrowBook(id);
        if (result)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Request arrived. Wait for the recepcionist's approval!!!");
        }
        else
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!You tried to borrow book that already borrowed. Please wait!!!");
        }
    }

    //for members to read
    private void ReadBookUI(Human human)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter book's Id: ");
        int id = UIHelper.GetValidIntWithinRange(1, Library.BookRepo.MyList.Count + 1);

        Book book = human.ReadBook(id);

        UIHelper.ReadBook(book);
    }

    //for managers to check new book
    private void ReadBookUI(Manager manager)
    {
        if (manager.PendingBooksToCreate.Count == 0)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!There is no pending requests!!!");
            return;
        }
        Book book = manager.PendingBooksToCreate.Peek();

        System.Console.WriteLine();
        System.Console.WriteLine($"Infos about book: Name: {book.Name}, Author: {String.Join(' ', book.Authors.Select(a => a.Name))}");
        System.Console.WriteLine();
        UIHelper.ReadBook(book);
        System.Console.WriteLine();
        System.Console.WriteLine("Do you want to add this book to library? (y/n)");

        var key = Console.ReadKey().Key;
        if (key == ConsoleKey.Y)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Book added!!!");
            manager.AddBookToLibrary();
        }
        else
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Book removed!!!");
            manager.PendingBooksToCreate.Dequeue();
        }
    }

    private void SeeBorrowRequestsUI(Recepcionist recepcionist)
    {
        if (recepcionist.BookBorrowRequests.Count == 0)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!There is no pending requests!!!");
            return;
        }
        BookRequest bookReq = recepcionist.BookBorrowRequests.Peek();
        Book book = Library.BookRepo.MyList.Find(b => b.Id == bookReq.BookId)!;
        Member member = (Member)Library.HumanRepo.MyList.Find(h => h.Id == bookReq.HumanId)!;
        System.Console.WriteLine();
        System.Console.WriteLine($"Member named {member.Name} is want to borrow the book called {book.Name}. Do you want to accept? (y/n)");
        var key = Console.ReadKey().Key;
        if (key == ConsoleKey.Y)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Request approved!!!");
            recepcionist.LendBookFor14Days();
        }
        else if (key == ConsoleKey.N)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("!!!Request removed!!!");
            recepcionist.BookBorrowRequests.Dequeue();
        }
    }

}