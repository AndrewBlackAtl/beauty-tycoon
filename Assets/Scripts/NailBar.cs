using System;
using UnityEditor;
using UnityEngine;

public class NailBar : Workplace, IWorkdayMember
{
    [SerializeField] private NailBarServiceUI ui;

    private Action<int> onServed;

    private void OnApplied(int option)
    {
        ui.SetActive(false);
        onServed?.Invoke(option);
    }

    public override void ServiceProcess(int desiredOption, Action<int> onServed)
    {
        this.onServed = onServed;
        ui.SetActive(true);
    }

    public void OnDayStart()
    {
        ui.OnApplied += OnApplied;
    }

    public void OnDayEnd()
    {
        ui.OnApplied -= OnApplied;
        ui.SetActive(false);
    }
}