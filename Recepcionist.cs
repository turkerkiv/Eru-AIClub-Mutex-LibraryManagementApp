
namespace LibraryManagementApp;

public class Recepcionist : Staff
{
    public Recepcionist(List<Weekdays> workDays, string shift, string password, string name, string surname, int age) : base(workDays, shift, password, name, surname, age)
    {
    }

    public void LendBook(Book book, int memberId)
    {

    }

    public string SignUpMember(Member member)
    {
        //add to memberrepo and if successfull
        //return random temp Password
        return string.Empty;
    }

    public void DeleteMember(Member member)
    {

    }

    public void AddBookToLibrary(Book book)
    {

    }

    public void RemoveBookFromLibrary(Book book)
    {

    }
}