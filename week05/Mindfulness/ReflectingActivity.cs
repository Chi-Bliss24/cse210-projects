using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ReflectingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private Random _rand = new Random();

        public ReflectingActivity() : base("Reflecting Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
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
            Console.WriteLine("When you have something in mind, press Enter to begin reflecting.");
            Console.ReadLine();

            DateTime end = DateTime.Now.AddSeconds(totalSeconds);
            while (DateTime.Now < end)
            {
                string question = GetRandomQuestion();
                Console.WriteLine($"- {question}");
                ShowSpinner(5); // pause + spinner between questions
                Console.WriteLine();
            }

            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            int idx = _rand.Next(_prompts.Count);
            return _prompts[idx];
        }

        private string GetRandomQuestion()
        {
            int idx = _rand.Next(_questions.Count);
            return _questions[idx];
        }
    }
}
