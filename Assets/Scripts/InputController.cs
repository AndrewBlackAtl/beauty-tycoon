using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float doubleTapThreshold;

    private Coroutine tapTimer;

    public event Action<Vector3> OnDoubleTap;

    private void Awake()
    {
        LeanTouch.OnFingerDown += OnTap;
    }

    private void OnTap(LeanFinger obj)
    {
        if (obj.IsOverGui)
        {
            return;
        }

        if (tapTimer != null)
        {
            StopCoroutine(tapTimer);
            tapTimer = null;
            OnDoubleTap?.Invoke(obj.ScreenPosition);
        }
        else
        {
            tapTimer = StartCoroutine(TapTimer());
        }
    }

    private IEnumerator TapTimer()
    {
        float time = 0f;
        while (time <= doubleTapThreshold)
        {
            time += Time.deltaTime;
            yield return null;
        }
        tapTimer = null;
    }
}
