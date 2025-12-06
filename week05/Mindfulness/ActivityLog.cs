using System;
using System.Collections.Generic;
using System.IO;

namespace MindfulnessApp
{
    // Simple persistent log that counts how many times each activity has been run.
    // Stores counts in a text file "activity_log.txt" in working directory.
    public class ActivityLog
    {
        private const string FileName = "activity_log.txt";
        private Dictionary<string, int> _counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public ActivityLog()
        {
            Load();
        }

        private void Load()
        {
            _counts.Clear();
            if (!File.Exists(FileName)) return;

            var lines = File.ReadAllLines(FileName);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int count))
                {
                    _counts[parts[0]] = count;
                }
            }
        }

        private void Save()
        {
            using (var sw = new StreamWriter(FileName, false))
            {
                foreach (var kvp in _counts)
                {
                    sw.WriteLine($"{kvp.Key}|{kvp.Value}");
                }
            }
        }

        public void Increment(string activityName)
        {
            if (!_counts.ContainsKey(activityName)) _counts[activityName] = 0;
            _counts[activityName]++;
            Save();
        }

        public int GetCount(string activityName)
        {
            return _counts.TryGetValue(activityName, out int c) ? c : 0;
        }

        public void DisplaySummary()
        {
            Console.WriteLine("--- Activity Log ---");
            if (_counts.Count == 0)
            {
                Console.WriteLine("No activities logged yet.");
                return;
            }

            foreach (var kvp in _counts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
