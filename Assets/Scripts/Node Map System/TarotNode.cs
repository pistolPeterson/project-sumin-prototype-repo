using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TarotNode :  INode
{
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        PlayNodeAudio();
        nodeMap.IncreaseProgress();
        SceneManager.LoadScene("CardSystemScene");
    }
    
    private void PlayNodeAudio()
    {
        var clip = AudioSOHandler.Instance.MapUIAudioSO.TarotCardBttn;
        AudioSOHandler.Instance.PlayOneShot(clip);
    }
}
