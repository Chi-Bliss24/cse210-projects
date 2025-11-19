using System;

// for showing Creativity and Exceeding Requirements, i added a list of verse to select from  at random. -line 30
class Program
{
    static void Main(string[] args)
    {
        {
            Console.Clear();
            Console.WriteLine("Scripture Memorization Program\n");

            // List of scripture references and texts
            List<Scripture> scriptureList = new List<Scripture>()
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life."
            ),
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding in all thy ways acknowledge him and he shall direct thy paths."
            ),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            )
        };

            // Picks a random verse form the list
            Random rnd = new Random();
            Scripture scripture = scriptureList[rnd.Next(scriptureList.Count)];

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress enter to continue or type 'quit' to exit.");
                string input = Console.ReadLine().ToLower();

                if (input == "quit")
                {
                    return;
                }

                scripture.HideRandomWords(3);

                if (scripture.IsCompletelyHidden())
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\nAll words are hidden. Program has ended.");
                    return;
                }
            }
        }
    }
}