using System;

class Cycling : Activity
{
    private double _speedMph; // stored speed in mph

    public Cycling(DateTime date, int minutes, double speedMph) : base(date, minutes)
    {
        _speedMph = speedMph;
    }

    public override double GetDistance()
    {
        // distance = speed (mph) * hours
        return _speedMph * ((double)Minutes / 60.0);
    }

    public override double GetSpeed()
    {
        return _speedMph;
    }

    public override double GetPace()
    {
        double distance = GetDistance();
        if (distance <= 0) return 0.0;
        return (double)Minutes / distance;
    }
}
