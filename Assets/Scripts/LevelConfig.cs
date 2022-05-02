using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [Header("Real time in minutes")]
    [SerializeField] private int dayDuration;

    [SerializeField] private int startHours;
    [SerializeField] private int startMinutes;

    [SerializeField] private int endHours;
    [SerializeField] private int endMinutes;

    [SerializeField] private float rentCost;

    public int DayDuration => dayDuration;
    public int StartHours => startHours;
    public int StartMinutes => startMinutes;
    public int EndHours => endHours;
    public int EndMinutes => endMinutes;
    public float RentCost => rentCost;
}
