using System.Collections;
using UnityEngine;
using Lean.Touch;
using System.Collections.Generic;
using System;

public class NailBarServiceUI : MonoBehaviour
{
    [SerializeField] private Transform cardsTransform;

    private int current = 1;
    private int numOfCards = 3; 

    public event Action<int> OnApplied;


    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnSwipe;
    }

    public void SetActive(bool value) 
    {
        gameObject.SetActive(value);
    }

    private void OnSwipe(LeanFinger obj)
    {
        if (obj.SwipeScreenDelta.x > 0f)
        {
            ApplyCurrent();
        }
        else
        {
            GetNext();
        }
    }

    private void GetNext()
    {
        if (current + 1 > numOfCards)
        {
            current = 1;
        }
        else
        {
            current++;
        }

        var first = cardsTransform.GetChild(cardsTransform.childCount - 1);
        first.SetAsFirstSibling();
    }

    private void ApplyCurrent()
    {
        OnApplied?.Invoke(current);
    }
}