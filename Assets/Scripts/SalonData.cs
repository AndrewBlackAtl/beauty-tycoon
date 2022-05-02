using System;
using UnityEditor;
using UnityEngine;

public class SalonData 
{
    private int totalVisitors;
    private int totalReputation;
    private float money;

    private float reputationRatio => totalVisitors > 0 ? (float)totalReputation / totalVisitors : 0;

    public float Money => money;
    public float ReputationRatio => reputationRatio;

    public event Action<float> OnMoneyChanged;
    public event Action<float> OnReputationChanged;

    public void ApplySeriveResult(IServiceResult result) 
    {
        AddMoney(result.MoneyChange);
        AddReputation(result.ReputationChange);
    }

    public void AddMoney(float value) 
    {
        money += value;
        OnMoneyChanged?.Invoke(money);
    }

    public void AddReputation(int value) 
    {
        totalVisitors++;
        totalReputation += value;
        OnReputationChanged?.Invoke(reputationRatio);
    }
}