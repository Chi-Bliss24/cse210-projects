using System;
using System.Threading;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration; // seconds

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _duration = 0;
        }

        public void SetDuration(int seconds)
        {
            if (seconds < 0) seconds = 0;
            _duration = seconds;
        }

        public int GetDuration()
        {
            return _duration;
        }

        public string GetName()
        {
            return _name;
        }

        public virtual void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"--- {_name} ---");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int seconds) || seconds < 0)
            {
                seconds = 0;
            }
            SetDuration(seconds);
            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        public virtual void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(2);
            Console.WriteLine($"You have completed the {GetName()} for {GetDuration()} seconds.");
            ShowSpinner(3);
            Console.WriteLine();
        }

        // Show a simple spinner for 'seconds' seconds
        public void ShowSpinner(int seconds)
        {
            char[] sequence = new char[] { '|', '/', '-', '\\' };
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < end)
            {
                Console.Write(sequence[i % sequence.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
                i++;
            }
        }

        // Show a countdown from seconds to 1 (each second)
        public void ShowCountDown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        // Run method implemented by subclasses
        public abstract void Run();
    }
}
