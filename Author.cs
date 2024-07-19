namespace LibraryManagementApp;

public class Author : Member
{
    public List<Page> CurrentPages { get; set; } = new();

    public Author(string password, string name, string surname, int age) : base(password, name, surname, age)
    {
    }

    public bool TryWritePage(string text)
    {
        string[] words = text.Split(' ');
        if (words.Length > 200) return false;
        Page newPage = new Page(text, CurrentPages.Count + 1);
        CurrentPages.Add(newPage);
        return true;
    }

    public Book CreateBook(string bookName)
    {
        Book book = new Book(bookName, CurrentPages, new List<Author> { this });
        CurrentPages = new();
        return book;
    }

    public void ImportPages()
    {
        //import pages from outside
    }
}