using System;
using System.Collections.Generic;

namespace YouTubeVideoProgram
{
    class Program
    {
        static void Main()
        {
            // Create some sample videos
            var videos = new List<Video>();

            var v1 = new Video("Gizmo Review: Everything You Need to Know", "TechWithTori", 645);
            v1.AddComment("Alex", "Great review — super helpful.");
            v1.AddComment("Maya", "I ordered one after watching this..");
            v1.AddComment("Sam", "You missed a key feature at 4:20.");
            videos.Add(v1);

            var v2 = new Video("5-Minute Vegan Pancakes", "CookAlongKay", 305);
            v2.AddComment("Jordan", "Perfect breakfast recipe!");
            v2.AddComment("Priya", "Tried it and my kids loved it.");
            v2.AddComment("Liamn", "Could you show a gluten-free alternative?");
            v2.AddComment("Rosa", "Nice voiceover and clear steps.");
            videos.Add(v2);

            var v3 = new Video("Hidden Gems in Lisbon — Travel Vlog", "WanderWithWill", 1180);
            v3.AddComment("Nina", "This makes me want to book a trip now!");
            v3.AddComment("Omar", "Beautiful cinematography.");
            v3.AddComment("Bea", "Where was the coffee shop at 7:10?");
            videos.Add(v3);

            var v4 = new Video("Unboxing & First Impressions: UltraPhone X", "GadgetGuru", 780);
            v4.AddComment("Chris", "Battery life test, please!");
            v4.AddComment("Taylor", "Is the headphone jack gone for good?");
            v4.AddComment("Hana", "Solid camera samples.");
            videos.Add(v4);

            // Display each video's info and comments
            foreach (var video in videos)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Title : {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.GetFormattedLength()} ({video.LengthSeconds} sec)");
                Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
                Console.WriteLine();

                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"   - {comment.Author}: {comment.Text}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("End of videos list. Press any key to exit...");
            Console.ReadKey();
        }
    }
}