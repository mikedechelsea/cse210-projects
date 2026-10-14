// Exceeds requirements in the following ways:
// 1. LEVEL SYSTEM: Every 1000 points earns the next level title
//    (Novice → Apprentice → Squire → Knight → Paladin → Champion → Legend).
//    The level and title are shown at the top of every menu.
// 2. NEGATIVE GOALS: A fourth goal type that subtracts points when a bad habit
//    is recorded (e.g. "Ate junk food" costs -50 points). Helps reinforce stopping
//    negative behaviors, not just building positive ones.
// 3. STREAK BONUS: Recording 5 or more events in a single session awards a
//    one-time 200-point bonus and a congratulatory message.

using System;
using System.Collections.Generic;
using System.IO;

// ── Base class ──────────────────────────────────────────────────────────────

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName() => _name;

    // Returns points earned (may be negative). Updates internal state.
    public abstract int RecordEvent();

    // One-line status shown in the goal list
    public abstract string GetDetailsString();

    // Returns true when the goal is fully complete (eternal goals never are)
    public abstract bool IsComplete();

    // Serialise to a single line for the save file
    public abstract string GetStringRepresentation();
}

// ── SimpleGoal ───────────────────────────────────────────────────────────────

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete) return 0;
        _isComplete = true;
        return _points;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString()
    {
        string status = _isComplete ? "[X]" : "[ ]";
        return $"{status} {_name} ({_description}) — {_points} pts";
    }

    public override string GetStringRepresentation() =>
        $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
}

// ── EternalGoal ──────────────────────────────────────────────────────────────

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => _points;

    public override bool IsComplete() => false;

    public override string GetDetailsString() =>
        $"[∞] {_name} ({_description}) — {_points} pts each time";

    public override string GetStringRepresentation() =>
        $"EternalGoal|{_name}|{_description}|{_points}";
}

// ── ChecklistGoal ─────────────────────────────────────────────────────────────

class ChecklistGoal : Goal
{
    private int _target;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points,
                         int target, int bonus, int currentCount = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _currentCount = currentCount;
    }

    public override int RecordEvent()
    {
        if (_currentCount >= _target) return 0;
        _currentCount++;
        return _currentCount == _target ? _points + _bonus : _points;
    }

    public override bool IsComplete() => _currentCount >= _target;

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";
        return $"{status} {_name} ({_description}) — {_points} pts, " +
               $"bonus {_bonus} at completion — Completed {_currentCount}/{_target} times";
    }

    public override string GetStringRepresentation() =>
        $"ChecklistGoal|{_name}|{_description}|{_points}|{_target}|{_bonus}|{_currentCount}";
}

// ── NegativeGoal (extra) ──────────────────────────────────────────────────────

class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int penalty)
        : base(name, description, penalty) { }

    public override int RecordEvent() => -_points;

    public override bool IsComplete() => false;

    public override string GetDetailsString() =>
        $"[-] {_name} ({_description}) — -{_points} pts (bad habit)";

    public override string GetStringRepresentation() =>
        $"NegativeGoal|{_name}|{_description}|{_points}";
}

// ── GoalManager ───────────────────────────────────────────────────────────────

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _sessionEvents = 0;
    private bool _streakBonusGiven = false;

    private static readonly string[] _levelTitles =
        { "Novice", "Apprentice", "Squire", "Knight", "Paladin", "Champion", "Legend" };

    public int GetScore() => _score;

    public string GetLevelTitle()
    {
        int level = _score / 1000;
        int idx = Math.Min(level, _levelTitles.Length - 1);
        return $"Level {level + 1} — {_levelTitles[idx]}";
    }

    public void CreateGoal(Goal goal) => _goals.Add(goal);

    public void RecordEvent()
    {
        if (_goals.Count == 0) { Console.WriteLine("No goals yet!"); return; }

        ListGoals();
        Console.Write("Which goal did you accomplish? (enter number): ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _goals.Count)
        {
            Console.WriteLine("Invalid selection."); return;
        }

        Goal goal = _goals[idx - 1];
        int earned = goal.RecordEvent();
        _score += earned;
        _sessionEvents++;

        if (earned > 0)
            Console.WriteLine($"\n+{earned} points! Total: {_score}");
        else if (earned < 0)
            Console.WriteLine($"\n{earned} points (bad habit recorded). Total: {_score}");
        else
            Console.WriteLine("\nGoal already complete — no points awarded.");

        if (goal.IsComplete())
            Console.WriteLine("*** Goal completed! Congratulations! ***");

        // Streak bonus: 5 events in one session
        if (_sessionEvents >= 5 && !_streakBonusGiven)
        {
            _streakBonusGiven = true;
            _score += 200;
            Console.WriteLine("\n*** SESSION STREAK BONUS: +200 points for recording 5 goals! ***");
        }
    }

    public void ListGoals()
    {
        if (_goals.Count == 0) { Console.WriteLine("No goals yet."); return; }
        Console.WriteLine();
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        Console.WriteLine();
    }

    public void SaveGoals(string filename)
    {
        using StreamWriter sw = new StreamWriter(filename);
        sw.WriteLine(_score);
        foreach (Goal g in _goals)
            sw.WriteLine(g.GetStringRepresentation());
        Console.WriteLine($"Saved to {filename}.");
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename)) { Console.WriteLine("File not found."); return; }
        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);
        _goals.Clear();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            switch (parts[0])
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]),
                        bool.Parse(parts[4])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                        int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])));
                    break;
                case "NegativeGoal":
                    _goals.Add(new NegativeGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
            }
        }
        Console.WriteLine($"Loaded {_goals.Count} goals. Score: {_score}");
    }
}

// ── Program ───────────────────────────────────────────────────────────────────

class Program
{
    static GoalManager manager = new GoalManager();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Score: {manager.GetScore()}  |  {manager.GetLevelTitle()}");
            Console.WriteLine("\n=== Eternal Quest Menu ===");
            Console.WriteLine("  1. Create new goal");
            Console.WriteLine("  2. List goals");
            Console.WriteLine("  3. Record event");
            Console.WriteLine("  4. Save goals");
            Console.WriteLine("  5. Load goals");
            Console.WriteLine("  6. Quit");
            Console.Write("\nChoice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": manager.ListGoals(); break;
                case "3": manager.RecordEvent(); break;
                case "4":
                    Console.Write("Filename: ");
                    manager.SaveGoals(Console.ReadLine());
                    break;
                case "5":
                    Console.Write("Filename: ");
                    manager.LoadGoals(Console.ReadLine());
                    break;
                case "6": return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("\nGoal types:");
        Console.WriteLine("  1. Simple Goal    (complete once)");
        Console.WriteLine("  2. Eternal Goal   (record every time, never complete)");
        Console.WriteLine("  3. Checklist Goal (complete N times for a bonus)");
        Console.WriteLine("  4. Negative Goal  (costs points — bad habit)");
        Console.Write("Type: ");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points per event: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                manager.CreateGoal(new SimpleGoal(name, desc, points));
                break;
            case "2":
                manager.CreateGoal(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("Target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus points on completion: ");
                int bonus = int.Parse(Console.ReadLine());
                manager.CreateGoal(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            case "4":
                manager.CreateGoal(new NegativeGoal(name, desc, points));
                break;
            default:
                Console.WriteLine("Unknown type.");
                break;
        }
    }
}
