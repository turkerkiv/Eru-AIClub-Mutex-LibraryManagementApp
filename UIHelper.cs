namespace LibraryManagementApp;

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
}