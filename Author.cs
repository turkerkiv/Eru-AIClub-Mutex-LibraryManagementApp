namespace LibraryManagementApp;

public class Author : Member
{
    public readonly List<Page> CurrentPages;

    public Author(string password, string name, string surname, int age) : base(password, name, surname, age)
    {
        CurrentPages = new();
    }

    public void WritePage(string text)
    {
        //create page obj and add to currentPages
    }

    public Book CreateBook(List<Page> pages)
    {
        //create book from currentpages and return
        return null!;
    }

    public void ImportPages()
    {
        //import pages from outside
    }
}