using System;
using MindfulnessApp;

class Program
{
    static void Main(string[] args)
    {
        ActivityLog log = new ActivityLog();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Start Breathing Activity");
                Console.WriteLine("2. Start Reflecting Activity");
                Console.WriteLine("3. Start Listing Activity");
                Console.WriteLine("4. View Activity Log");
                Console.WriteLine("5. Quit");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine();
                Activity selected = null;

                switch (choice.Trim())
                {
                    case "1":
                        selected = new BreathingActivity();
                        break;
                    case "2":
                        selected = new ReflectingActivity();
                        break;
                    case "3":
                        selected = new ListingActivity();
                        break;
                    case "4":
                        Console.WriteLine();
                        log.DisplaySummary();
                        Console.WriteLine();
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        continue;
                    case "5":
                        running = false;
                        continue;
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                }

                selected.Run();
                log.Increment(selected.GetName());

                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }

            Console.WriteLine("Goodbye. Take care!");
        }
    }
