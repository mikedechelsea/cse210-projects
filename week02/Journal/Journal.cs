using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();
    
    
    public List<string> _prompts = new List<string>
    {
        "What was the best part of my day?",
        "What new thing did you learn today?",
        "What was the most challenging part of my day?",
        "What am I grateful for today?",
        "What is one thing I can do to improve tomorrow?",
        "What is a positive affirmation I can tell myself today?"
    };

    
    public string GetRandomPrompt()
    {
        Random pickRandomly = new Random();
        int index = pickRandomly.Next(_prompts.Count); 
        return _prompts[index];
    }

    public void Display()
{
    Console.WriteLine("\n--- Journal Entries ---");
    foreach (Entry entry in _entries)
    {
        entry.Display();
        Console.WriteLine();
    }
}

public void SaveToFile(string fileName)
{
    StreamWriter writer = new StreamWriter(fileName);
    foreach (Entry entry in _entries)
    {
        writer.WriteLine($"{entry._entryDate}|{entry._prompt}|{entry._userResponse}");
    }
    writer.Close();
}

public void LoadFromFile(string fileName)
{
    _entries.Clear();

    StreamReader reader = new StreamReader(fileName);

    string line = reader.ReadLine();

    while (line != null)
    {
        string[] parts = line.Split('|');

        if (parts.Length == 3)
        {
            Entry entry = new Entry();
            entry._entryDate = parts[0];
            entry._prompt = parts[1];
            entry._userResponse = parts[2];

            _entries.Add(entry);
        }

        line = reader.ReadLine();
    }

    reader.Close();
}
}

