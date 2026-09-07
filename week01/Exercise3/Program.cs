using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string playAgain;

        // Outer loop: Runs the entire game again if they type "yes"
        do
        {
            // Generate a fresh random number for every new game round
            int guessNumber = random.Next(1, 101); 

            Console.Write("Choose a magic number between 1 and 100: ");
            string number = Console.ReadLine();
            int userNumber = int.Parse(number);

            // Inner loop: Keeps asking until they guess the correct number
            while (userNumber != guessNumber)
            {
                if (userNumber > guessNumber)
                {
                    Console.WriteLine($"Your guess number is {userNumber} and is higher than the magic number");
                }
                else if (userNumber < guessNumber)
                {
                    Console.WriteLine($"Your guess number is {userNumber} and is lower than the magic number");
                }

                // Ask for a new guess since they haven't gotten it yet
                Console.Write("Choose a magic number between 1 and 100: ");
                number = Console.ReadLine();
                userNumber = int.Parse(number);
            }

            // If the code reaches here, they broke out of the inner loop (They won!)
            Console.WriteLine($"Congratulations! The magic number was indeed {userNumber}.");

            // Prompt to play again
            Console.Write("Do you wish to play again? (yes/no): ");
            playAgain = Console.ReadLine()?.ToLower();

        } while (playAgain == "yes");

        Console.WriteLine("Thank you for playing!");
    }
}
