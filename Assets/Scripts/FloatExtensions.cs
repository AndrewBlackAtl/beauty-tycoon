using System;

public static class FloatExtensions
{
    public static string RatioToPercentString(this float value, int round = 1)
    {
        return string.Concat(Math.Round(value * 100, round), '%');
    }
}