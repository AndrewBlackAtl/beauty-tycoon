using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public event Action<TimeSpan> TimeUpdatedEvent;
    public event Action TimeIsUpEvent;

    private TimeSpan startTime;
    private TimeSpan current;
    private double timeMultiplier;
    private double levelDuration;

    public void Setup(TimeSpan startTime, TimeSpan delta, TimeSpan realDuration)  
    {
        this.startTime = startTime;
        timeMultiplier = delta / realDuration;
        levelDuration = realDuration.TotalSeconds;
    }

    public void StartTimeProcess() 
    {
        current = startTime;
        StartCoroutine(TimeProcess(levelDuration, timeMultiplier));
    }

    private IEnumerator TimeProcess(double duration, double multiplier) 
    {
        double time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            current += TimeSpan.FromSeconds(Time.deltaTime * multiplier);
            TimeUpdatedEvent?.Invoke(current);
            yield return null;
        }
        TimeIsUpEvent?.Invoke();
    }
}
