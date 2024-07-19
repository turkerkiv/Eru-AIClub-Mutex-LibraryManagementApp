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
}