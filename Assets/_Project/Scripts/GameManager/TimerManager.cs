using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : GenericSingleton<TimerManager>
{
    public event Action<float> OnTimeChanged;

    public float CurrentTime { get; private set; }

    private void SetTime(float time)
    {
        CurrentTime = Mathf.Max(time, 0);
        CurrentTime = time;
    }

    private void UpdateTimer()
    {
        SetTime(CurrentTime += Time.deltaTime);
        OnTimeChanged?.Invoke(CurrentTime);
    }

    private void Update()
    {
        UpdateTimer();
    }
}
