using System;
using System.Collections.Generic;
using System.Linq; // Required for LINQ operations

class Program
{
    static void Main(string[] args)
    {
        List<int> numberList = new List<int>();

        Console.WriteLine("Enter positive or negative numbers. Enter '0' to stop and view results.\n");

        while (true)
        {
            Console.Write("Enter a number (Positive or Negative): ");
            string userNumber = Console.ReadLine();

            // 1. Try to convert the input into an integer
            if (int.TryParse(userNumber, out int number))
            {
                // 2. Check if the input is 0 to exit the loop
                if (number == 0)
                {
                    Console.WriteLine("\nYou've exited the program.");
                    break; 
                }

                // 3. If it's any other number, add it to the list
                numberList.Add(number); 
            }
            else
            {
                // Handles bad inputs (like letters or symbols) gracefully
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        Console.WriteLine("\n---------------------------------------------------------");

        // 4. Perform calculations only if the list has items
        if (numberList.Count > 0)
        {
            int numberSum = numberList.Sum();
            double numAverage = numberList.Average();
            int numMax = numberList.Max();
            int numMin = numberList.Min();
            var newestFirst = numberList.AsEnumerable().Reverse();

            Console.WriteLine($"The sum of numbers in the list is: {numberSum}");
            Console.WriteLine($"The average is: {numAverage}");
            Console.WriteLine($"The largest number is: {numMax}");
            Console.WriteLine($"The smallest number is: {numMin}");
            
            Console.WriteLine($"\nNumbers entered (Newest First): {string.Join(", ", newestFirst)}");
        }
        else
        {
            Console.WriteLine("No numbers (other than 0) were entered, so no statistics could be calculated.");
        }
    }
}
