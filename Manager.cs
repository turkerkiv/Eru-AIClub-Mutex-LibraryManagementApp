
namespace LibraryManagementApp;

public class Manager : Staff
{
    public Manager(List<Weekdays> workDays, string shift, string password, string name, string surname, int age) : base(workDays, shift, password, name, surname, age)
    {

    }

    public void AddBookToLibrary(Book book)
    {

    }

    public void RemoveBookFromLibrary(Book book)
    {

    }

    public bool TryHireStaff(Human human, List<Weekdays> workdays, string shift)
    {
        if (human is Staff staff) return false;

        Library.HumanRepo.MyList.Remove(human);
        Recepcionist recepcionist = new Recepcionist(workdays, shift, human.Password, human.Name, human.Surname, human.Age);
        recepcionist.Id = human.Id;
        Library.HumanRepo.MyList.Add(recepcionist);
        return true;
    }

    public void FireStaff(Staff staff)
    {
        Library.HumanRepo.MyList.Remove(staff);
    }
}