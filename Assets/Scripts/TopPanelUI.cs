using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TopPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockLabel;
    [SerializeField] private TextMeshProUGUI reputationLabel;
    [SerializeField] private TextMeshProUGUI currencyLabel;

    public void Setup(TimeSpan startTime, float reputation, float money)
    {
        clockLabel.text = startTime.ToAMPMFormat();
        reputationLabel.text = reputation.RatioToPercentString();
        currencyLabel.text = money.ToString();
    }

    public void OnTimeUpdate(TimeSpan value)
    {
        clockLabel.text = value.ToAMPMFormat();
    }

    public void OnReputationChanged(float value)
    {
        reputationLabel.text = value.RatioToPercentString();
    }

    public void OnMoneyChanged(float value)
    {
        currencyLabel.text = Math.Round(value, 2).ToString();
    }
}