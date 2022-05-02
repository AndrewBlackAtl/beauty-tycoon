using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class TimeSpanExtensions
{
    public static string ToAMPMFormat(this TimeSpan time) 
    {
        DateTime dateTime = new DateTime(time.Ticks);
        return dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
    }
}
