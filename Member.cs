namespace LibraryManagementApp;

public class Member : Human
{
    public Member(string password, string name, string surname, int age) : base(password, name, surname, age)
    {
    }

    public Book BorrowBook(string bookName)
    {
        //search book from repo and send request to recepcionist
        return null!;
    }

    public string RequestNewBook(string bookName)
    {
        //send request to recepcionist
        return string.Empty;
    }
}