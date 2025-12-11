using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var activities = new List<Activity>();

        // Create one of each activity (no user input, as requested)
        activities.Add(new Running(new DateTime(2025, 11, 03), 30, 3.0));    // 3.0 miles in 30 min
        activities.Add(new Cycling(new DateTime(2025, 11, 03), 45, 12.0));   // 12 mph for 45 min
        activities.Add(new Swimming(new DateTime(2025, 11, 03), 30, 40));    // 40 laps (50m each) in 30 min

        // Iterate and print summaries polymorphically
        foreach (var act in activities)
        {
            Console.WriteLine(act.GetSummary());
        }
    }
}
