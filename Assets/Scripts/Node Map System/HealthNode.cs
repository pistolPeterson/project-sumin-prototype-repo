using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : INode
{
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        
        PlayNodeAudio();
        Debug.Log("we healing");
        nodeMap.IncreaseProgress();
        GameManager.Instance.willHealThisRound = true;


    }

    private void PlayNodeAudio()
    {
        var clip = AudioSOHandler.Instance.MapUIAudioSO.HeatlhBttn;
        AudioSOHandler.Instance.PlayOneShot(clip);
    }
}
