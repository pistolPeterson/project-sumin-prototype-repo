using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class BaseAudio : MonoBehaviour
{
    protected AudioSource baseAudio;
    public List<AudioClip> baseAudioClips;
    [SerializeField] protected bool playOnStart = false;

    public virtual void Start()
    {
        InitializeAudio();
    }

    public virtual void InitializeAudio()
    {
        baseAudio = GetComponent<AudioSource>();
        if (playOnStart)
            PlayAudio();
    }

    [ProButton]
    public virtual void PlayAudio()
    {
        if (baseAudioClips.Count > 0)
        {
            baseAudio.PlayOneShot(baseAudioClips[Random.Range(0, baseAudioClips.Count)]);
            Debug.Log("tried to play");
        }
    }
}
