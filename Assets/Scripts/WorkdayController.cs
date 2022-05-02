using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWorkdayMember 
{
    public void OnDayStart();
    public void OnDayEnd();
}


public class WorkdayController
{
    private List<IWorkdayMember> members = new List<IWorkdayMember>();
    private GameTime gameTime;

    public WorkdayController(GameTime gameTime) 
    {
        this.gameTime = gameTime;
        this.gameTime.TimeIsUpEvent += TimeIsUp;
    }

    private void TimeIsUp()
    {
        DayEnd();
    }

    public void AddMember(IWorkdayMember member) 
    {
        members.Add(member);
    }

    public void DayStart() 
    {
        gameTime.StartTimeProcess();
        foreach (var item in members)
        {
            item.OnDayStart();
        }
    }

    private void DayEnd() 
    {
        foreach (var item in members)
        {
            item.OnDayEnd();
        }
    }
}