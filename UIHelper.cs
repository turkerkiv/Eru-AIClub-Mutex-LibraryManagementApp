namespace LibraryManagementApp;

public static class UIHelper
{
    public static int GetValidInteger()
    {
        bool isNum = false;
        int num = 0;
        while (!isNum)
        {
            System.Console.WriteLine("Please enter a number!");
            isNum = int.TryParse(Console.ReadLine(), out num);
        }
        return num;
    }
}