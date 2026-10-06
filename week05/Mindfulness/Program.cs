// Exceeds requirements in two ways:
// 1. No-repeat prompts/questions: prompts and questions are shuffled so every item
//    is shown before any repeats occur within a session.
// 2. Activity log: each completed activity is appended to "mindfulness_log.txt"
//    with the date, activity name, and duration. A 4th "Log" menu option lets the
//    user view the log without leaving the app.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class MindfulnessActivity
{
    private string _name;
    private string _description;
    protected int _duration;

    public MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string GetName() => _name;

    protected void ShowStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"=== {_name} ===\n");
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How many seconds would you like for this activity? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("\nGet ready to begin...");
        ShowSpinner(3);
    }

    protected void ShowEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou have completed {_duration} seconds of the {_name}.");
        ShowSpinner(3);
        ActivityLog.Record(_name, _duration);
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($" {i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b\b   \b\b\b");
        }
    }

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        int ticks = seconds * 4;
        for (int i = 0; i < ticks; i++)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }
}

// Shuffled-bag helper so prompts don't repeat until all are used
class ShuffleBag<T>
{
    private List<T> _source;
    private Queue<T> _bag;
    private Random _rng = new Random();

    public ShuffleBag(IEnumerable<T> items)
    {
        _source = new List<T>(items);
        _bag = new Queue<T>();
    }

    public T Next()
    {
        if (_bag.Count == 0)
        {
            var shuffled = _source.OrderBy(_ => _rng.Next()).ToList();
            foreach (var item in shuffled)
                _bag.Enqueue(item);
        }
        return _bag.Dequeue();
    }
}

static class ActivityLog
{
    private static readonly string LogFile = "mindfulness_log.txt";

    public static void Record(string activityName, int duration)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm} | {activityName,-22} | {duration} seconds";
        File.AppendAllText(LogFile, entry + Environment.NewLine);
    }

    public static void Display()
    {
        Console.Clear();
        Console.WriteLine("=== Activity Log ===\n");
        if (!File.Exists(LogFile) || new FileInfo(LogFile).Length == 0)
        {
            Console.WriteLine("No activities logged yet.");
        }
        else
        {
            Console.WriteLine(File.ReadAllText(LogFile));
        }
        Console.Write("Press Enter to return to menu...");
        Console.ReadLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    public void Run()
    {
        ShowStartingMessage();
        int elapsed = 0;
        bool breatheIn = true;
        while (elapsed < _duration)
        {
            int pauseSeconds = Math.Min(4, _duration - elapsed);
            if (breatheIn)
                Console.Write("\nBreathe in...");
            else
                Console.Write("\nBreathe out...");
            ShowCountdown(pauseSeconds);
            elapsed += pauseSeconds;
            breatheIn = !breatheIn;
        }
        ShowEndingMessage();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> Questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private ShuffleBag<string> _promptBag = new ShuffleBag<string>(Prompts);
    private ShuffleBag<string> _questionBag = new ShuffleBag<string>(Questions);

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }

    public void Run()
    {
        ShowStartingMessage();
        Console.WriteLine($"\n{_promptBag.Next()}\n");
        Console.WriteLine("Reflect on the following questions as they appear...");
        ShowSpinner(3);

        int elapsed = 0;
        while (elapsed < _duration)
        {
            string q = _questionBag.Next();
            Console.WriteLine($"\n> {q}");
            int pauseSeconds = Math.Min(5, _duration - elapsed);
            ShowSpinner(pauseSeconds);
            elapsed += pauseSeconds;
        }
        ShowEndingMessage();
    }
}

class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private ShuffleBag<string> _promptBag = new ShuffleBag<string>(Prompts);

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    public void Run()
    {
        ShowStartingMessage();
        Console.WriteLine($"\n{_promptBag.Next()}");
        Console.WriteLine("\nYou have a few seconds to think before you begin listing...");
        ShowCountdown(5);

        Console.WriteLine("\nStart listing items. Press Enter after each one.\n");

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                items.Add(input);
        }

        Console.WriteLine($"\nYou listed {items.Count} item(s)!");
        ShowEndingMessage();
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Breathing Activity");
            Console.WriteLine("  2. Reflection Activity");
            Console.WriteLine("  3. Listing Activity");
            Console.WriteLine("  4. View Activity Log");
            Console.WriteLine("  5. Quit");
            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": new BreathingActivity().Run(); break;
                case "2": new ReflectionActivity().Run(); break;
                case "3": new ListingActivity().Run(); break;
                case "4": ActivityLog.Display(); break;
                case "5": return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
