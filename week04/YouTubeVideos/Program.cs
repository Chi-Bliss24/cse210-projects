using System;
using System.Collections.Generic;

namespace YouTubeVideoProgram
{

    public class Comment
    {
        public string Author { get; }
        public string Text { get; }

        public Comment(string author, string text)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString()
        {
            return $"{Author}: {Text}";
        }
    }

    // Represents a YouTube video that holds its title, author, length, and comments
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        /// <summary>Length in seconds</summary>
        public int LengthSeconds { get; set; }

        private readonly List<Comment> _comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            LengthSeconds = lengthSeconds >= 0 ? lengthSeconds : throw new ArgumentOutOfRangeException(nameof(lengthSeconds));
        }

        // Add a comment object to this video
        public void AddComment(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            _comments.Add(comment);
        }

        // Convenience overload to add by author and text
        public void AddComment(string author, string text)
        {
            AddComment(new Comment(author, text));
        }

        // Returns the number of comments for the video
        public int GetNumberOfComments()
        {
            return _comments.Count;
        }

        // Provide read-only access to comments (so callers can iterate)
        public IReadOnlyList<Comment> GetComments()
        {
            return _comments.AsReadOnly();
        }

        // video time as mm:ss for nicer display
        public string GetFormattedLength()
        {
            int minutes = LengthSeconds / 60;
            int seconds = LengthSeconds % 60;
            return $"{minutes}:{seconds:D2}";
        }
    }

    class Program
    {
        static void Main()
        {
            // Create some sample videos
            var videos = new List<Video>();

            var v1 = new Video("Gizmo Review: Everything You Need to Know", "TechWithTori", 645);
            v1.AddComment("Alex", "Great review — super helpful.");
            v1.AddComment("Maya", "I ordered one after watching this.");
            v1.AddComment("Sam", "You missed a key feature at 4:20.");
            videos.Add(v1);

            var v2 = new Video("5-Minute Vegan Pancakes", "CookAlongKay", 305);
            v2.AddComment("Jordan", "Perfect breakfast recipe!");
            v2.AddComment("Priya", "Tried it and my kids loved it.");
            v2.AddComment("Liam", "Could you show a gluten-free alternative?");
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
                    Console.WriteLine($"  - {comment.Author}: {comment.Text}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("End of videos list. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
