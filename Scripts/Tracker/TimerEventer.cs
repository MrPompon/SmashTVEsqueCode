using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEventer
{
    //public actions
    public System.Action OnTimerReached, OnTimerReset;

    //private variables
    private float progress, cooldown, originCooldown;

    //Contructor
    public TimerEventer(float cooldown, float startProgress = 0)
    {
        this.originCooldown = cooldown;
        this.cooldown = this.originCooldown;
        this.progress = startProgress;
    }
    //public methods
    public void DoUpdate(float time, bool autoResetTimer = false)
    {
        progress += time;
        if (IsTimeBeyondCooldown())
        {
            SendOnTimerReached();
            if (autoResetTimer)
            {
                ResetTimer();
            }
        }
    }
    public void ResetTimer()
    {
        progress = 0;
        SendOnTimerReset();
    }
    public bool IsTimeBeyondCooldown()
    {
        if (progress >= cooldown)
        {
            return true;
        }
        return false;
    }
    public float GetProgress()
    {
        return progress;

    }
    public void SetProgress(float progress)
    {
        this.progress = progress;
    }
    public float GetCooldown()
    {
        return cooldown;
    }
    //private methods
    private void SendOnTimerReached()
    {
        OnTimerReached?.Invoke();
    }
    private void SendOnTimerReset()
    {
        OnTimerReset?.Invoke();
    }
}