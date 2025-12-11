using System;

abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    protected string DateString => _date.ToString("dd MMM yyyy");
    protected int Minutes => _minutes;

    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Polymorphic methods — declared here, implemented in derived classes.
    public abstract double GetDistance(); // miles
    public abstract double GetSpeed();    // mph
    public abstract double GetPace();     // minutes per mile

    // Common summary that uses the above methods (available to all derived types)
    public virtual string GetSummary()
    {
        double distance = GetDistance();
        double speed = GetSpeed();
        double pace = GetPace();

        // Protect against division by zero / zero-distance summaries
        string distanceStr = distance > 0 ? $"{distance:F1} miles" : "0.0 miles";
        string speedStr = speed > 0 ? $"{speed:F1} mph" : "0.0 mph";
        string paceStr = (distance > 0) ? $"{pace:F2} min per mile" : "N/A";

        return $"{DateString} {GetType().Name} ({Minutes} min) - Distance {distanceStr}, Speed {speedStr}, Pace: {paceStr}";
    }
}
