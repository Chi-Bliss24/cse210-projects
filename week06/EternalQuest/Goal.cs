using System;

abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    protected Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    // Called when user records an event for this goal. Returns points earned (including bonuses).
    public abstract int RecordEvent();

    // Whether the goal is fully complete (for EternalGoal this is always false).
    public abstract bool IsComplete();

    // Short details for display e.g. "[ ] Read Scripture (Eternal) Completed: --"
    public abstract string GetDetailsString();

    // A serialization-friendly single line representation for saving to file.
    // Derived classes should override and include type marker as first token.
    public abstract string ToSaveString();

    // Factory method to restore a Goal from saved tokens.
    public static Goal FromSaveString(string line)
    {
        // Expected format: type|... tokens depending on type
        // We'll split and parse in derived-specific logic
        var parts = line.Split('|');
        if (parts.Length < 1) throw new Exception("Bad save line: " + line);

        string type = parts[0];
        switch (type)
        {
            case "Simple":
                // Simple|name|description|points|isComplete
                if (parts.Length < 5) throw new Exception("Bad Simple save line.");
                var sName = parts[1];
                var sDesc = parts[2];
                var sPoints = int.Parse(parts[3]);
                var sIsComplete = bool.Parse(parts[4]);
                var sg = new SimpleGoal(sName, sDesc, sPoints);
                if (sIsComplete)
                {
                    // mark complete but avoid awarding points now
                    sg.ForceCompleteWithoutPoints();
                }
                return sg;

            case "Eternal":
                // Eternal|name|description|points
                if (parts.Length < 4) throw new Exception("Bad Eternal save line.");
                var eName = parts[1];
                var eDesc = parts[2];
                var ePoints = int.Parse(parts[3]);
                return new EternalGoal(eName, eDesc, ePoints);

            case "Checklist":
                // Checklist|name|description|points|amountCompleted|target|bonus
                if (parts.Length < 7) throw new Exception("Bad Checklist save line.");
                var cName = parts[1];
                var cDesc = parts[2];
                var cPoints = int.Parse(parts[3]);
                var cAmountCompleted = int.Parse(parts[4]);
                var cTarget = int.Parse(parts[5]);
                var cBonus = int.Parse(parts[6]);
                var cg = new ChecklistGoal(cName, cDesc, cPoints, cTarget, cBonus);
                cg.SetAmountCompleted(cAmountCompleted);
                return cg;

            default:
                throw new Exception("Unknown goal type: " + type);
        }
    }
}
