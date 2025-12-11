using System;
using System.Collections.Generic;
using System.IO;

class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private LevelSystem _levelSystem = new LevelSystem();


    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public int Score => _score;

    public int Level => _levelSystem.Level;
    public IReadOnlyList<string> Badges => _levelSystem.Badges;

    public void CreateGoal(Goal g)
    {
        _goals.Add(g);
        Console.WriteLine($"Added goal: {g.GetDetailsString()}");
    }

    public void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.");
            return;
        }
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine($"Score: {_score}");
        ListGoalNames();
    }

    public void RecordEvent(int index)
    {
        if (index < 1 || index > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }
        var g = _goals[index - 1];
        int awarded = g.RecordEvent();
        _score += awarded;
        _levelSystem.Update(_score);

    }

    public void SaveGoals(string filename)
    {
        using (var writer = new StreamWriter(filename))
        {
            // First line: score
            writer.WriteLine(_score);
            writer.WriteLine(_levelSystem.ToSaveString());

            // Following lines: serialized goals
            foreach (var g in _goals)
            {
                writer.WriteLine(g.ToSaveString());
            }
        }
        Console.WriteLine($"Goals saved to {filename}");
    }

    public void LoadGoals(string filename)
    {
    if (!File.Exists(filename))
    {
        Console.WriteLine($"File {filename} not found.");
        return;
    }

    string[] lines = File.ReadAllLines(filename);
    if (lines.Length == 0)
    {
        Console.WriteLine("Save file empty.");
        return;
    }

    _goals.Clear();

    // 1️⃣ Load score from first line
    int.TryParse(lines[0], out _score);

    // 2️⃣ Check if second line contains LEVEL information
    int startIndex;
    if (lines.Length > 1 && lines[1].StartsWith("LEVEL"))
    {
        _levelSystem.LoadFromString(lines[1]);
        startIndex = 2;  // Goals begin after level line
    }
    else
    {
        startIndex = 1;  // No level line → goals start immediately
    }

    // 3️⃣ Load goals starting at correct position
    for (int i = startIndex; i < lines.Length; i++)
    {
        if (string.IsNullOrWhiteSpace(lines[i])) continue;

        try
        {
            var g = Goal.FromSaveString(lines[i]);
            _goals.Add(g);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load line {i + 1}: {ex.Message}");
        }
    }

    // 4️⃣ Re-apply level updates so player is consistent
    _levelSystem.Update(_score);

    Console.WriteLine($"Loaded {_goals.Count} goals. Current score: {_score}");
    }

}
