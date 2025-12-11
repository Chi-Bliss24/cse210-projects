using System;
using System.Collections.Generic;

class LevelSystem
{
    private int _level;
    private List<string> _badges;

    // Score thresholds for levels (you can change these!)
    private int[] _levelThresholds = { 0, 500, 1000, 2000, 5000, 10000 };

    public int Level => _level;
    public IReadOnlyList<string> Badges => _badges.AsReadOnly();

    public LevelSystem()
    {
        _level = 1;
        _badges = new List<string>();
    }

    // Call this every time the score changes
    public void Update(int score)
    {
        int oldLevel = _level;

        for (int i = _levelThresholds.Length - 1; i >= 0; i--)
        {
            if (score >= _levelThresholds[i])
            {
                _level = i + 1; // Level numbers start at 1
                break;
            }
        }

        if (_level > oldLevel)
        {
            Console.WriteLine($"🎉 Level Up! You reached Level {_level}!");
            AwardBadge($"Reached Level {_level}");
        }

        CheckScoreBadges(score);
    }

    private void CheckScoreBadges(int score)
    {
        if (score >= 1000 && !_badges.Contains("Bronze Score Medal"))
            AwardBadge("Bronze Score Medal");
        if (score >= 3000 && !_badges.Contains("Silver Score Medal"))
            AwardBadge("Silver Score Medal");
        if (score >= 7000 && !_badges.Contains("Gold Score Medal"))
            AwardBadge("Gold Score Medal");
    }

    private void AwardBadge(string badge)
    {
        _badges.Add(badge);
        Console.WriteLine($"🏅 New Badge Earned: {badge}");
    }

    // Save badges + level to file
    public string ToSaveString()
    {
        return $"LEVEL|{_level}|{string.Join(";", _badges)}";
    }

    // Load from save file
    public void LoadFromString(string line)
    {
        var parts = line.Split('|');

        _level = int.Parse(parts[1]);

        _badges.Clear();
        if (parts.Length > 2 && parts[2].Length > 0)
        {
            _badges.AddRange(parts[2].Split(';'));
        }
    }
}
