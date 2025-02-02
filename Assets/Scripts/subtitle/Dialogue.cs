using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;
using STOP_MODE = FMOD.Studio.STOP_MODE;

/// <summary>
/// data file to hold audio and the relevant text
/// </summary>

[CreateAssetMenu(fileName = "Dialogue Instance", menuName = "Create Dialogue Object")]
public class Dialogue : ScriptableObject
{
     [TextAreaAttribute] public string Text = "wow! you found super secret dialogue!";
     public EventReference dialogueAudio;
     private FMOD.Studio.EventInstance dialogueAudioState;

     public void PlayAudio()
     {
         dialogueAudioState = FMODUnity.RuntimeManager.CreateInstance(dialogueAudio);
         dialogueAudioState.start();
     }

     public void StopAudio()
     {
         dialogueAudioState.stop(STOP_MODE.ALLOWFADEOUT);
     }
     
     public float GetAudioLength()
     {
         if (!dialogueAudioState.isValid())
             return 0;
         
         FMOD.Studio.EventDescription evt;
         dialogueAudioState.getDescription(out evt);
         int len = 0;
         evt.getLength(out len);
        
         var finalLength = len * 0.0006f;
         Debug.Log("Length of audio clip " + finalLength);
         return finalLength;
     }
}
