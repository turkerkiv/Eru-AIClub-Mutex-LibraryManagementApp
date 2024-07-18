namespace LibraryManagementApp;

public class Repository<T>
{
    public readonly List<T> MyList;

    public Repository()
    {
        //will get list from databasemaanger
        MyList = new List<T>();
    }
}