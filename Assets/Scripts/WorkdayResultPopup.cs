using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WorkdayResultPopup : MonoBehaviour, IWorkdayMember
{
    [SerializeField] private TextMeshProUGUI visitorsPerDayLabel;
    [SerializeField] private TextMeshProUGUI moneyPerDayLabel;
    [SerializeField] private TextMeshProUGUI reputationPerDayLabel;
    [SerializeField] private TextMeshProUGUI rentCostLabel;

    private WorkdayCounter counter;

    public event Action OnClickStartNewDay;

    public void Setup(WorkdayCounter counter, float rentCost) 
    {
        this.counter = counter;

        rentCostLabel.text = string.Concat("Rent: -", rentCost);
    }


    public void OnDayStart()
    {
        gameObject.SetActive(false);
    }

    public void OnDayEnd()
    {
        var data = counter.Data;
        visitorsPerDayLabel.text = string.Concat("Visitors served: ", data.VisitorsPerDay);
        moneyPerDayLabel.text = string.Concat("Money: +", data.MoneyPerDay);
        reputationPerDayLabel.text = string.Concat("Reputation: ", ((float)data.ReputationPerDay / data.VisitorsPerDay).RatioToPercentString());

        gameObject.SetActive(true);
    }

    public void OnStartNewDay() 
    {
        OnClickStartNewDay?.Invoke();
    }
}