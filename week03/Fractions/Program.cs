using System;

class Program
{
    static void Main(string[] args)
    {
        // Constructor 1: no parameters -> 1/1
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        // Constructor 2: one parameter -> 5/1
        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        // Constructor 3: two parameters -> 3/4
        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        // Constructor 3: two parameters -> 1/3
        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());

        // Test getters and setters
        Fraction f5 = new Fraction(2, 5);
        Console.WriteLine($"Before: {f5.GetFractionString()}");
        f5.SetTop(3);
        f5.SetBottom(8);
        Console.WriteLine($"After:  {f5.GetFractionString()}");
    }
}
