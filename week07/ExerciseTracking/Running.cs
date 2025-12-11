using System;

class Running : Activity
{
    private double _distanceMiles;

    public Running(DateTime date, int minutes, double distanceMiles) : base(date, minutes)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistance()
    {
        return _distanceMiles;
    }

    public override double GetSpeed()
    {
        // mph = (distance / minutes) * 60
        if (Minutes <= 0) return 0.0;
        return (GetDistance() / Minutes) * 60.0;
    }

    public override double GetPace()
    {
        // minutes per mile = minutes / distance
        double distance = GetDistance();
        if (distance <= 0) return 0.0;
        return (double)Minutes / distance;
    }
}
