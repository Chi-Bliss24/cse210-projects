using System;
// Excceding requirement: Added functionality for viewing levels and badges earned.
class Program
{
    static void Main()
    {
        var manager = new GoalManager();
        string filename = "goals.txt";

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Eternal Quest — Menu");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event (complete a goal)");
            Console.WriteLine("4. Display score");
            Console.WriteLine("5. Save goals");
            Console.WriteLine("6. Load goals");
            Console.WriteLine("7. View levels & badges");
            Console.WriteLine("8. Exit");

            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoalFlow(manager);
                    break;
                case "2":
                    manager.ListGoalDetails();
                    break;
                case "3":
                    manager.ListGoalNames();
                    Console.Write("Enter goal number to record: ");
                    if (int.TryParse(Console.ReadLine(), out int idx))
                    {
                        manager.RecordEvent(idx);
                    }
                    else Console.WriteLine("Bad input.");
                    break;
                case "4":
                    Console.WriteLine($"Total score: {manager.Score}");
                    break;
                case "5":
                    Console.Write($"Filename to save [{filename}]: ");
                    var saveName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(saveName)) filename = saveName;
                    manager.SaveGoals(filename);
                    break;
                case "6":
                    Console.Write($"Filename to load [{filename}]: ");
                    var loadName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(loadName)) filename = loadName;
                    manager.LoadGoals(filename);
                    break;

                case "7":
                    DisplayLevels(manager);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;

            }
        }
    }

    static void CreateGoalFlow(GoalManager manager)
    {
        Console.WriteLine("Goal Types: 1) Simple  2) Eternal  3) Checklist");
        Console.Write("Choose type: ");
        var t = Console.ReadLine();
        Console.Write("Short name: ");
        var name = Console.ReadLine();
        Console.Write("Description: ");
        var desc = Console.ReadLine();
        Console.Write("Points (per event or completion): ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points.");
            return;
        }

        switch (t)
        {
            case "1":
                var s = new SimpleGoal(name, desc, points);
                manager.CreateGoal(s);
                break;
            case "2":
                var e = new EternalGoal(name, desc, points);
                manager.CreateGoal(e);
                break;
            case "3":
                Console.Write("Target (how many times to complete): ");
                if (!int.TryParse(Console.ReadLine(), out int target))
                {
                    Console.WriteLine("Invalid target.");
                    return;
                }
                Console.Write("Bonus points (awarded when target reached): ");
                if (!int.TryParse(Console.ReadLine(), out int bonus))
                {
                    Console.WriteLine("Invalid bonus.");
                    return;
                }
                var c = new ChecklistGoal(name, desc, points, target, bonus);
                manager.CreateGoal(c);
                break;
            default:
                Console.WriteLine("Unknown type.");
                break;
        }
        
    }

    static void DisplayLevels(GoalManager manager)
    {
        Console.WriteLine($"Current Level: {manager.Level}");
        Console.WriteLine("Badges Earned:");
        if (manager.Badges.Count == 0)
        {
            Console.WriteLine("   (none yet)");
        }
        else
        {
            foreach (var b in manager.Badges)
                Console.WriteLine($"   - {b}");
        }
    }


}
