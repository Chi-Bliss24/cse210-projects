using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private Random _rand = new Random();

        public ListingActivity() : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();

            int totalSeconds = GetDuration();
            if (totalSeconds <= 0)
            {
                Console.WriteLine("No time set. Returning to menu.");
                ShowSpinner(2);
                return;
            }

            string prompt = GetRandomPrompt();
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine();
            Console.WriteLine("You will have a few seconds to think about your answers. Prepare...");
            ShowCountDown(5);
            Console.WriteLine();
            Console.WriteLine("Start listing items (press Enter after each).");

            DateTime end = DateTime.Now.AddSeconds(totalSeconds);
            List<string> entries = new List<string>();

            while (DateTime.Now < end)
            {
                // Calculate remaining time for prompt display purposes only (user input may block).
                Console.Write("> ");
                // To avoid blocking longer than the time allotment, we still accept user input;
                // if the user lingers we will finish counting when they press Enter.
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    entries.Add(line.Trim());
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {entries.Count} items:");
            foreach (string e in entries)
            {
                Console.WriteLine($" - {e}");
            }

            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            int idx = _rand.Next(_prompts.Count);
            return _prompts[idx];
        }
    }
}
