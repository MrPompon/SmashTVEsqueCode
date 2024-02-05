using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InAudioSystem;

public class Trigger_Sound : MonoBehaviour
{
    public InAudioNode audioToPlay;
    public Transform playLocation;

    public Trigger trigger;

    private void Awake()
    {
        trigger.OnTriggered += Do;
    }
    private void OnDestroy()
    {
        trigger.OnTriggered -= Do;
    }
    void Do()
    {
        if (playLocation != null && audioToPlay != null)
        {
            InAudio.Play(playLocation.transform.gameObject, audioToPlay);
        }

    }
}
