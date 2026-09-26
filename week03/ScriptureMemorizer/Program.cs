// Exceeds Requirements:
// 1. Stretch challenge: HideRandomWords only picks from words not yet hidden,
//    so no wasted turns re-hiding already-hidden words.
// 2. Scripture library: the program picks a random scripture from a list of five
//    each time it runs, so the user practices a different verse every session.

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> library = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"),
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding In all thy ways acknowledge him and he shall direct thy paths"),
            new Scripture(new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me"),
            new Scripture(new Reference("Joshua", 1, 9),
                "Be strong and of a good courage be not afraid neither be thou dismayed for the Lord thy God is with thee whithersoever thou goest"),
            new Scripture(new Reference("Romans", 8, 28),
                "And we know that all things work together for good to them that love God to them who are the called according to his purpose")
        };

        Random random = new Random();
        Scripture scripture = library[random.Next(library.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
                break;

            Console.Write("Press Enter to continue or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);
        }

        Console.WriteLine("\nGoodbye!");
    }
}
