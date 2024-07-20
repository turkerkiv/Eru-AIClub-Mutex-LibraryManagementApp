namespace LibraryManagementApp.View;

public static class UIHelper
{
    public static int GetValidInteger()
    {
        int num = 0;
        bool isNum = int.TryParse(Console.ReadLine(), out num);
        while (!isNum)
        {
            System.Console.WriteLine("Please enter a number!");
            isNum = int.TryParse(Console.ReadLine(), out num);
        }
        return num;
    }

    public static int GetValidIntWithinRange(int listCount)
    {
        int num = GetValidInteger();
        bool isWithinRange = listCount > num && num > 0;
        while (!isWithinRange)
        {
            num = GetValidInteger();
            isWithinRange = listCount > num && num > 0;
        }
        return num;
    }

    public static void ReadBook(Book book)
    {
        int page = 0;
        do
        {
            System.Console.WriteLine("This book is " + book.PageCount + " pages long. Enter page number to read or 0 to exit");
            page = UIHelper.GetValidIntWithinRange(book.PageCount + 1);

            string text = book.Pages[page - 1].Text ?? "";
            System.Console.WriteLine("----------------Page " + page + "----------------");
            System.Console.WriteLine(text);
            System.Console.WriteLine("----------------Page " + page + "----------------");
        }
        while (page != 0);
    }
}