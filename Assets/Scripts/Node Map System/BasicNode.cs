using System.Collections;
using System.Collections.Generic;
using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicNode : INode
{
   
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        //TODO: verify data for next encounter
        PlayNodeAudio();
        nodeMap.IncreaseProgress();
      TransitionManager.Instance.LoadLevel("MainEncounter");
    }
    
    private void PlayNodeAudio()
    {
        var clip = AudioSOHandler.Instance.MapUIAudioSO.EncounterBttn;
        AudioSOHandler.Instance.PlayOneShot(clip);
    }
}
