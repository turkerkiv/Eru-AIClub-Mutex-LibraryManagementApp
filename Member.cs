namespace LibraryManagementApp;

public class Member : Human
{
    public Member(string password, string name, string surname, int age) : base(password, name, surname, age)
    {
    }

    public void BorrowBook(int id)
    {
        Book bookToBorrow = Library.BookRepo.MyList.Find(b => b.Id == id)!;
        Random rnd = new Random();
        List<Recepcionist> recepcionists = Library.HumanRepo.MyList.OfType<Recepcionist>().ToList();
        Recepcionist r = recepcionists[rnd.Next(recepcionists.Count)];
        r.GetBorrowRequest(this, bookToBorrow);
    }
}