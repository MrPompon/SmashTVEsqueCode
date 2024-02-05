using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_ChangeMusic : GameEvent
{
    public AudioClip clip;
    public bool playOnSwap = true;
    public float playDelay = 0;

    private void OnValidate()
    {
        if (clip == null)
        {
            Debug.LogError("needs audioclip");
        }
    }
    protected override void EventTriggered()
    {
        //base.EventTriggered();
        if (clip == null)
            return;

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Couldnt find camera?", this);
            return;
        }
        AudioSource source = mainCamera.GetComponent<AudioSource>();
        if (source != null )
        {
            source.clip = clip;
            if (playOnSwap)
                source.PlayDelayed(playDelay);
        }
    }
}
