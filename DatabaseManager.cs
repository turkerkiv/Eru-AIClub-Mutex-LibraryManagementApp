using System.Reflection;
using System.Text.Json;

namespace LibraryManagementApp;

public static class DatabaseManager
{
    private readonly static string path = @"C:\Software-Projects\LibraryManagementApp\JsonData\@plchld.txt";

    public static void LoadAllRepos()
    {
        if (File.Exists(CreatePath("humans")))
        {
            string json = ReadFromFile("humans");
            List<HumanWrapper> humans = JsonSerializer.Deserialize<List<HumanWrapper>>(json)!;
            humans.ForEach(humanWrapper =>
            {
                Type type = Type.GetType($"LibraryManagementApp.{humanWrapper.TypeName}")!;
                Human hmn = (Human) JsonSerializer.Deserialize(humanWrapper.JsonData, type)!;
                System.Console.WriteLine(hmn.GetType());
                Library.HumanRepo.MyList.Add(hmn);
            });
        }
        if (File.Exists(CreatePath("books")))
        {
            string json = ReadFromFile("books");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(json)!;
            Library.BookRepo.MyList.AddRange(books);
        }
    }

    public static void SaveAllRepos()
    {
        var humanWrappers = Library.HumanRepo.MyList.Select(h => new HumanWrapper
        {
            TypeName = h.GetType().Name,
            JsonData = JsonSerializer.Serialize(h),
        });

        string humansJson = JsonSerializer.Serialize(humanWrappers);
        string booksJson = JsonSerializer.Serialize(Library.BookRepo.MyList);

        WriteToFile(humansJson, "humans");
        WriteToFile(booksJson, "books");
    }

    private static void WriteToFile(string json, string name)
    {
        StreamWriter str = new StreamWriter(CreatePath(name), false);
        str.WriteLine(json);
        str.Close();
    }

    private static string ReadFromFile(string name)
    {
        StreamReader str = new StreamReader(CreatePath(name));
        string json = str.ReadToEnd();
        str.Close();
        return json;
    }

    private static string CreatePath(string name)
    {
        return path.Replace("@plchld", name);
    }

    public class HumanWrapper
    {
        public string TypeName { get; set; }
        public string JsonData { get; set; }
    }
}