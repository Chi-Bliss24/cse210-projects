using System;
using System.Collections.Generic;

namespace YouTubeVideoProgram
{
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
}