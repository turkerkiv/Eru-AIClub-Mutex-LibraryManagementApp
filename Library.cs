namespace LibraryManagementApp;

public class Library
{
    public readonly Repository<Book> BookRepo;
    public readonly Repository<Staff> StaffRepo;
    public readonly Repository<Member> MemberRepo;

    public Library()
    {
        BookRepo = new Repository<Book>();
        StaffRepo = new Repository<Staff>();
        MemberRepo = new Repository<Member>();
    }
}