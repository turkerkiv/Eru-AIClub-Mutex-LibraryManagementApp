namespace LibraryManagementApp;

public abstract class Human
{
    public int Id { get; }
    public string Password { get; private set;}
    public string Name { get; }
    public string Surname { get; }
    public int Age { get; }

    public Human(string password, string name, string surname, int age)
    {
        //Set ID here by looking previous ones
        Password = password;
        Name = name;
        Surname = surname;
        Age = age;
    }

    public void ChangePassword(string newPassword)
    {
        Password = newPassword;
    }
}