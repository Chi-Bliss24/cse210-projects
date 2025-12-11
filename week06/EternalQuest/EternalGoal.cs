using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        Console.WriteLine($"Recorded '{_shortName}'. +{_points} points (eternal).");
        return _points;
    }

    public override bool IsComplete()
    {
        // eternal goals never considered "complete"
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[∞] {_shortName} (Eternal) -- {_description}";
    }

    public override string ToSaveString()
    {
        // Eternal|name|description|points
        return $"Eternal|{Escape(_shortName)}|{Escape(_description)}|{_points}";
    }

    private string Escape(string s) => s.Replace("|", "\\|");
}
