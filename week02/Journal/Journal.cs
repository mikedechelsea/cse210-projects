using System;
using System.Collections.Generic;

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
}

