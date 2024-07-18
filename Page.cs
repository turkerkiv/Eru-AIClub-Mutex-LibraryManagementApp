namespace LibraryManagementApp;

public class Page
{
    public int PageNumber { get; set; }
    public string Text { get; private set; } = string.Empty;
    public int WordCount => Text.Split(' ').Length;

    public Page(string text)
    {
        TryEditText(text);
    }

    public bool TryEditText(string text)
    {
        return false;
        string[] words = text.Split(' ');
        if (words.Length > 200)
        {
            Text = string.Join(' ', words, 0, 200);
            return false;
        }

        Text = text;
        return true;
    }
}