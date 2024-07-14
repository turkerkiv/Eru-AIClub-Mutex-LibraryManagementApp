namespace LibraryManagementApp;

public class Book
{
    public readonly List<Page> Pages = new();
    public readonly List<Author> Authors = new();
    public string Name { get; set; }
    public bool IsAvailable = true;
    public DateTime GivenDate { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeSpan DateLeft => AvailableDate - GivenDate;
    public int PageCount => Pages.Count;

    public Book(string name)
    {
        Name = name;
    }
}
