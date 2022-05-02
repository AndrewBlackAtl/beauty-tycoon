using System;
using System.Collections;
using UnityEngine;

public abstract class Workplace : MonoBehaviour
{
    [SerializeField] private Transform workPosition;
    [SerializeField] private Transform servicePosition;

    public Transform WorkPosition => workPosition;
    public Transform ServicePosition => servicePosition;

    public abstract void ServiceProcess(int desiredOption, Action<int> onServed);
}