using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAudio : BaseAudio
{
    [SerializeField] private NodeMapButton nodeMapButton;

    public override void Start()
    {
        base.Start();
        nodeMapButton.OnNodePointerEnter.AddListener(PlayEnterNodeAudio);
        nodeMapButton.OnNodePointerExit.AddListener(PlayExitNodeAudio);
    }

    private void PlayEnterNodeAudio()
    {
        
        baseAudio.PlayOneShot(baseAudioClips[0]);
    }
    private void PlayExitNodeAudio()
    {
        
        baseAudio.PlayOneShot(baseAudioClips[1]);
    }
}
