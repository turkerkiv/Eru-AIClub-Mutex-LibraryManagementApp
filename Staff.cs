namespace LibraryManagementApp;

public abstract class Staff : Human
{
    //Change here to enumerable
    public List<string> WorkDays { get; }
    public string Shift { get; set; }

    public Staff(List<string> workDays, string shift, string password, string name, string surname, int age) : base(password, name, surname, age)
    {
        WorkDays = new();
        workDays.ForEach(d => WorkDays.Add(d));
        Shift = shift;
    }
}