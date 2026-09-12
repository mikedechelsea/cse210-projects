using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Display welcome message
        string message = WelcomeMessage();
        Console.WriteLine(message);

        // 2. Get user's name
        string name = ReturnUserName();
        
        // 3. Get user's number
        int number = ReturnUserNumber();

        // 4. Calculate the square of the number
        int square = ReturnSquareNumber(number);

        // 5. Generate the final display string using the name and square
        string display = ReturnDisplay(name, square);

        // 6. Print the results
        Console.WriteLine(display);
    }

    
    static string WelcomeMessage()
    {
        return "Welcome to the program!";
    }

    static string ReturnUserName()
    {
        Console.Write("What's your name? ");
        string userName = Console.ReadLine();
        return userName; // Return the actual input typed by the user
    }

    static int ReturnUserNumber()
    {
        Console.Write("Enter a number to square: ");
        string userNumber = Console.ReadLine();
        
        // Converts the text input into an integer
        int number = int.Parse(userNumber); 
        return number;
    }

    // Accepts the number as an input parameter, squares it, and returns the result
    static int ReturnSquareNumber(int number)
    {
        
        int numberSquare = number * number; 
        return numberSquare;
    }

    // Accepts both the name and the squared number to construct the final message
    static string ReturnDisplay(string name, int squareResult)
    {
        string display = $"{name}, the square of your number is {squareResult}";
        return display;
    }
}
