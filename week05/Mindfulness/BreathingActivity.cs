using System;

namespace MindfulnessApp
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
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

            DateTime endTime = DateTime.Now.AddSeconds(totalSeconds);
            bool inhale = true;

            while (DateTime.Now < endTime)
            {
                if (inhale)
                {
                    Console.Write("Breathe in... ");
                }
                else
                {
                    Console.Write("Breathe out... ");
                }

                // Use a 4-second paced breath by default (or remaining seconds if less)
                int remaining = (int)(endTime - DateTime.Now).TotalSeconds;
                int pause = Math.Min(4, Math.Max(1, remaining));

                ShowCountDown(pause);
                Console.WriteLine();
                inhale = !inhale;
            }

            DisplayEndingMessage();
        }
    }
}
