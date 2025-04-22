using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AudioSOHandler : Singleton<AudioSOHandler>
{
    public TarotAudioSO TarotAudioSO;
    public MainMenuAudioSO MainMenuAudioSO;
    public EncounterAudioSO EncounterAudioSO;
    public MapUIAudioSO MapUIAudioSO;


    public float PlayOneShot(EventReference audioClip)
    {
        if (audioClip.IsNull)
        {
            Debug.Log("No Audio clip to play");
            return 0;

        }
        
        RuntimeManager.PlayOneShot(audioClip);
        return GetAudioLength(audioClip);

    }
    
    private float GetAudioLength(EventReference audioClip)
    {
       var dialogueAudioState = FMODUnity.RuntimeManager.CreateInstance(audioClip);
        
        if (!dialogueAudioState.isValid())
            return 0;
         
        FMOD.Studio.EventDescription evt;
        dialogueAudioState.getDescription(out evt);
        int len = 0;
        evt.getLength(out len);
        
        var finalLength = len * 0.0006f;
        return finalLength;
    }

}
