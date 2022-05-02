using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelInitializator : MonoBehaviour
{
    [SerializeField] private LevelConfig config;
    [SerializeField] private float serviceDelay;

    [SerializeField] private GameUI gameUI;

    [SerializeField] private VisitorSpawner spawner;
    [SerializeField] private List<Workplace> workplaces;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        var gameTime = new GameObject("GameTime").AddComponent<GameTime>();
        gameTime.Setup(new TimeSpan(config.StartHours, config.StartMinutes, 0),
            new TimeSpan(config.EndHours, config.EndMinutes, 0) - new TimeSpan(config.StartHours, config.StartMinutes, 0),
            new TimeSpan(0, config.DayDuration, 0));
        
        var salonData = new SalonData();
        gameTime.TimeIsUpEvent += () => salonData.AddMoney(-config.RentCost);

        var workdayCounter = new WorkdayCounter();
        spawner.Setup(workplaces, serviceDelay);
        spawner.OnVisitorServedEvent += workdayCounter.OnVisitorServed;
        spawner.OnVisitorServedEvent += salonData.ApplySeriveResult;

        gameUI.TopPanelUI.Setup(new TimeSpan(config.StartHours, config.StartMinutes, 0), salonData.ReputationRatio, salonData.Money);
        gameUI.WorkdayResultPopup.Setup(workdayCounter, config.RentCost);
        
        gameTime.TimeUpdatedEvent += gameUI.TopPanelUI.OnTimeUpdate;
        salonData.OnReputationChanged += gameUI.TopPanelUI.OnReputationChanged;
        salonData.OnMoneyChanged += gameUI.TopPanelUI.OnMoneyChanged;

        var workdayController = new WorkdayController(gameTime);
        gameUI.WorkdayResultPopup.OnClickStartNewDay += workdayController.DayStart;
        workdayController.AddMember(workdayCounter);
        workdayController.AddMember(spawner);
        workdayController.AddMember(gameUI.WorkdayResultPopup);

        foreach (var item in workplaces)
        {
            if (item is IWorkdayMember member)
            {
                workdayController.AddMember(member);
            }
        }
        workdayController.DayStart();
    }
}