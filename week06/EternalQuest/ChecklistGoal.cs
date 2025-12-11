using System;

class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) 
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    // Used when loading
    public void SetAmountCompleted(int amount)
    {
        _amountCompleted = amount;
        if (_amountCompleted > _target) _amountCompleted = _target;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("Checklist goal already complete. No points.");
            return 0;
        }

        _amountCompleted++;
        int totalAward = _points;
        Console.WriteLine($"Recorded '{_shortName}' ({_amountCompleted}/{_target}). +{_points} points.");

        if (_amountCompleted >= _target)
        {
            // award bonus once when reaching target
            totalAward += _bonus;
            Console.WriteLine($"Congratulations! Checklist finished — bonus +{_bonus} points!");
        }

        return totalAward;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string check = IsComplete() ? "[X]" : "[ ]";
        return $"{check} {_shortName} (Checklist) -- {_description} Completed {_amountCompleted}/{_target}";
    }

    public override string ToSaveString()
    {
        // Checklist|name|description|points|amountCompleted|target|bonus
        return $"Checklist|{Escape(_shortName)}|{Escape(_description)}|{_points}|{_amountCompleted}|{_target}|{_bonus}";
    }

    private string Escape(string s) => s.Replace("|", "\\|");
}
