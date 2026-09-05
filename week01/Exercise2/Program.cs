using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise2 Project.");

        Console.Write("Enter Your Score: ");
        string userScore = Console.ReadLine();
        int score = int.Parse(userScore);

        string letterGrade = "";
        string sign = "";

        // 1. Determine base letter grade
        if (score >= 90)
        {
            letterGrade = "A";
        }
        else if (score >= 80)
        {
            letterGrade = "B";
        }
        else if (score >= 70)
        {
            letterGrade = "C";
        }
        else if (score >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }

        // 2. Determine sign modifier (+ or -)
        int lastDigit = score % 10;

        if (letterGrade != "F") // F grades don't receive a + or -
        {
            if (lastDigit >= 7 || score >= 100)
            {
                sign = "+";
            }
            else if (lastDigit <= 2)
            {
                sign = "-";
            }
        }

        // 3. Print result
        string finalGrade = letterGrade + sign;

        if (letterGrade == "D" || letterGrade == "F")
        {
            Console.WriteLine($"You scored {score} and your grade is {finalGrade}. You Failed!");
        }
        else
        {
            Console.WriteLine($"You scored {score} and your grade is {finalGrade}.");
        }
    }
}