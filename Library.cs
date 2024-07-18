namespace LibraryManagementApp;

public static class Library
{
    public static readonly Repository<Book> BookRepo = new();
    public static readonly Repository<Human> HumanRepo = new();
}