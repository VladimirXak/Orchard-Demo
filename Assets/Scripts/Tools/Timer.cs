using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction OnEndTime;
    public event UnityAction<string> OnTickTimer;

    private Coroutine coroutineTimer;

    public void StartTimer(DateTime dateEnd)
    {
        Stop();

        coroutineTimer = StartCoroutine(TickTimer(dateEnd));
    }

    public void Stop()
    {
        if (coroutineTimer != null)
        {
            StopCoroutine(coroutineTimer);
        }
    }

    private IEnumerator TickTimer(DateTime dateEnd)
    {
        while (true)
        {
            TimeSpan timeSpan = dateEnd - DateTime.Now;
            int totalSeconds = (int)timeSpan.TotalSeconds;

            if (totalSeconds <= 0)
            {
                OnEndTime?.Invoke();
                coroutineTimer = null;
                yield break;
            }
            else
            {
                OnTickTimer?.Invoke(ToString(totalSeconds));
            }

            yield return null;
        }
    }

    private string ToString(int seconds)
    {
        if (seconds < 3600)
            return GetMinutes(seconds);
        else
            return GetHours(seconds);
    }

    private string GetMinutes(int seconds)
    {
        int totalMinuts = seconds / 60;
        int remainderSeconds = seconds % 60;

        return $"{totalMinuts}m {remainderSeconds.ToString("D2")}s";
    }

    private string GetHours(int seconds)
    {
        int totalHours = seconds / 3600;
        int remainderMinutes = (seconds % 3600)/60;

        return $"{totalHours}h {remainderMinutes.ToString("D2")}m";
    }
}
