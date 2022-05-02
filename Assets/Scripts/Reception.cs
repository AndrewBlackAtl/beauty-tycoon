using System;
using UnityEditor;
using UnityEngine;

public class Reception : Workplace
{
    public override void ServiceProcess(int desiredOption, Action<int> onServed)
    {
        onServed?.Invoke(desiredOption);
    }
}