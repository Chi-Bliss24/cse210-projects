using System;

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    // Used when loading a goal that was already completed: mark it completed without awarding points.
    public void ForceCompleteWithoutPoints()
    {
        _isComplete = true;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("That goal is already completed. No points awarded.");
            return 0;
        }
        _isComplete = true;
        Console.WriteLine($"Goal '{_shortName}' completed! +{_points} points.");
        return _points;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        string check = _isComplete ? "[X]" : "[ ]";
        return $"{check} {_shortName} (Simple) -- {_description}";
    }

    public override string ToSaveString()
    {
        // Simple|name|description|points|isComplete
        return $"Simple|{Escape(_shortName)}|{Escape(_description)}|{_points}|{_isComplete}";
    }

    private string Escape(string s) => s.Replace("|", "\\|");
}
