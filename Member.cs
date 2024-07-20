namespace LibraryManagementApp;

public class Member : Human
{
    public List<Book> BorrowedBooks { get; set; } = new();

    public Member(string password, string name, string surname, int age) : base(password, name, surname, age)
    {
    }

    public bool TryBorrowBook(int id)
    {
        Book bookToBorrow = Library.BookRepo.MyList.Find(b => b.Id == id)!;
        if (!bookToBorrow.IsAvailable) return false;

        Random rnd = new Random();
        List<Recepcionist> recepcionists = Library.HumanRepo.MyList.OfType<Recepcionist>().ToList();
        Recepcionist r = recepcionists[rnd.Next(recepcionists.Count)];
        r.GetBorrowRequest(this, bookToBorrow);
        return true;
    }
}