using System;

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
}