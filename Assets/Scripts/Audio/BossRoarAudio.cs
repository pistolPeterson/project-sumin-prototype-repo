using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossRoarAudio : MonoBehaviour
{
    private AudioSource baseAudio;
    [SerializeField] private List<AudioClip> baseAudioClips;
    [SerializeField] private bool playOnStart = false;

    [SerializeField] private BossAttackHandler bossAttackHandler;
    private void Start()
    {
        InitializeAudio();
        bossAttackHandler?.OnSpecialPerformed.AddListener(PlayAudio);
    }

    private void InitializeAudio()
    {
        baseAudio = GetComponent<AudioSource>();
        if (playOnStart)
            PlayAudio();
    }

    [ProButton]
    private void PlayAudio()
    {
        if(baseAudioClips.Count > 0)
            baseAudio.PlayOneShot(baseAudioClips[Random.Range(0, baseAudioClips.Count)]);
    }
}
