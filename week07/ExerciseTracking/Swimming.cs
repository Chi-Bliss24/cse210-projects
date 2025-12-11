using System;

class Swimming : Activity
{
    private int _laps;          // number of 50-meter laps
    private const double MetersPerLap = 50.0;
    private const double MetersToKm = 1.0 / 1000.0;
    private const double KmToMiles = 0.62; // as specified in assignment

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // laps * 50 meters -> km -> miles
        double km = _laps * MetersPerLap * MetersToKm;
        double miles = km * KmToMiles;
        return miles;
    }

    public override double GetSpeed()
    {
        double minutes = Minutes;
        if (minutes <= 0) return 0.0;
        // mph = (distance / minutes) * 60
        return (GetDistance() / minutes) * 60.0;
    }

    public override double GetPace()
    {
        double distance = GetDistance();
        if (distance <= 0) return 0.0;
        return (double)Minutes / distance;
    }
}
