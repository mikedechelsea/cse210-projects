using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Initialize the journal first so we can access its features
        Journal myJournal = new Journal();

        // 2. Create the new entry
        Entry newEntry = new Entry();

        // 3. Use the helper method we built to get a random prompt safely
        newEntry._prompt = myJournal.GetRandomPrompt();

        // 4. Show the prompt to the user and capture the response
        Console.WriteLine(newEntry._prompt);
        Console.Write("> ");
        string response = Console.ReadLine() ?? "";

        // 5. Populate the entry details
        newEntry._userResponse = response;
        newEntry._entryDate = DateTime.Now.ToShortDateString(); // Cleaner date format
        
        // 6. Save it to the journal and display
        myJournal._entries.Add(newEntry);
        myJournal.Display();
    }
}
