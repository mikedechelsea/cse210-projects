using System;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Enter Your First Name?");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter Your Last Name?");
        string lastName = Console.ReadLine();

        Console.WriteLine($"Your Name Is {firstName}, {firstName} {lastName}");


    }
}