namespace LibraryManagementApp;

public class Repository<T>
{
    public readonly List<T> MyList;

    public Repository()
    {
        MyList = new List<T>();
    }

    public void AddToList(T obj)
    {
        // MyList.Add(obj);
    }

    public void RemoveFromList(T obj)
    {
        // MyList.Remove(obj);
    }
}