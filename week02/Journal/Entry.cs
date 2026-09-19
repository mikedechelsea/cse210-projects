using System;

public class Entry
{
    public string _prompt = "";
    public string _userResponse = "";
    public string _entryDate = ""; 

    public void Display()
    {
        Console.WriteLine($"{_entryDate}: {_prompt}");
        Console.WriteLine($"Response: {_userResponse}");
    }
}
