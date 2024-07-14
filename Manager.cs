
namespace LibraryManagementApp;

public class Manager : Staff
{
    public Manager(List<string> workDays, string shift, string password, string name, string surname, int age) : base(workDays, shift, password, name, surname, age)
    {

    }

    public void HireStaff(Staff staff)
    {
        //just access to staff repo and add or remove
    }

    public void FireStaff(Staff staff)
    {

    }
}