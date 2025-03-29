
using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AmbiOneShotMono : MonoBehaviour
{
    private AmbientOneShot ambientOneShot;
    private float timer = 0f;
    private EventInstance eventInstance;
    public void SetAmbientOneShot(AmbientOneShot _ambientOneShot)
    {
        this.ambientOneShot = _ambientOneShot;
        ambientOneShot.SetRandomWaitTime(0);

    }
    
    

    private void Update()
    {
        if (ambientOneShot == null)
            return;
        timer += Time.deltaTime;

        if (timer > ambientOneShot.GetLastWaitTime())
        {
                
            ambientOneShot.SetRandomWaitTime(GetAudioClipLength());
            timer = 0;
            RuntimeManager.PlayOneShot(ambientOneShot.eventClip);
        }
        
    }
    
    private float GetAudioClipLength()
    {
         eventInstance = RuntimeManager.CreateInstance(ambientOneShot.eventClip);
         if (!eventInstance.isValid())
            return 0;
         
         FMOD.Studio.EventDescription evt;
         eventInstance.getDescription(out evt);
         int len = 0;
         evt.getLength(out len);
        
         var finalLength = len * 0.0006f;
         return finalLength;
    }
}
