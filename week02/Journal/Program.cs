using System;


class Program
{
    static void Main(string[] args)
    {
        // 1. Initialize the journal first
        Journal myJournal = new Journal();
        bool running = true;

        while (running)
        {
            // 2. Show the menu INSIDE the loop so it repeats after every action
            Console.WriteLine("\n1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            
            // int.TryParse prevents the program from crashing if the user types a letter
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue; 
            }

            switch (choice)
            {
                case 1:
                    // Variables declared here are scoped safely to this block
                    Entry newEntry = new Entry();
                    newEntry._prompt = myJournal.GetRandomPrompt();
                    
                    Console.WriteLine(newEntry._prompt);
                    Console.Write("> ");
                    
                    newEntry._userResponse = Console.ReadLine() ?? "";
                    newEntry._entryDate = DateTime.Now.ToShortDateString();
                    
                    myJournal._entries.Add(newEntry);
                    break;

                case 2:
                    myJournal.Display();
                    break;

                case 3:
                    Console.WriteLine("CASE 3 IS RUNNING");
                    Console.Write("Enter the filename to save the journal: ");
                    string saveFileName = Console.ReadLine();
                    Console.WriteLine($"Filename received: {saveFileName}");
                    Console.WriteLine(Environment.CurrentDirectory);
                    myJournal.SaveToFile(saveFileName);
                    Console.WriteLine("Journal saved successfully.");
                    break;

                case 4:
                    Console.WriteLine("CASE 4 IS RUNNING");
                    Console.Write("Enter the filename to load the journal from: ");
                    string loadFileName = Console.ReadLine();
                    Console.WriteLine($"Filename received: {loadFileName}");
                    myJournal.LoadFromFile(loadFileName);
                    Console.WriteLine("Journal loaded successfully.");
                    break;

                case 5:
                    running = false;
                    break;
                    
                default:
                    Console.WriteLine("Please select a valid option.");
                    break;
            }
        }
    }
}