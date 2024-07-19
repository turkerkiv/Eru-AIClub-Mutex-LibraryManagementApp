
namespace LibraryManagementApp;

public class Recepcionist : Staff
{
    public Queue<BookRequest> BookBorrowRequests { get; set; } = new();

    public Recepcionist(List<Weekdays> workDays, string shift, string password, string name, string surname, int age) : base(workDays, shift, password, name, surname, age)
    {
    }

    public void RequestBorrowBook(Human human, Book book)
    {
        BookBorrowRequests.Enqueue(new BookRequest(book.Id, human.Id));
    }

    public void LendBook()
    {
        BookRequest bq = BookBorrowRequests.Dequeue();
        Book book = Library.BookRepo.MyList.Find(b => b.Id == bq.BookId)!;
        book.GivenDate = DateTime.Now;
        book.IsAvailable = false;
        book.AvailableDate = DateTime.Now.AddDays(14);
    }

    public void TakeBookBack(Book book)
    {
        book.IsAvailable = true;
        book.GivenDate = null;
        book.AvailableDate = null;
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
}