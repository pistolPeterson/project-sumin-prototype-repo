using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
   private AudioSource musicSource;
   [SerializeField] private AudioClip mainMenuClip;
   [SerializeField] private float fadeInTime = 2.5f;
   [SerializeField] private float snapshotFade = 0.5f;
   [SerializeField] private AudioMixerSnapshot mainSnapShot;
   [SerializeField] private AudioMixerSnapshot pausedSnapShot;
   private void Start()
   {
      musicSource = GetComponent<AudioSource>();
      musicSource.clip = mainMenuClip;
      musicSource.volume = 0f;
      musicSource.DOFade(1f, fadeInTime);
      musicSource.Play();
      mainSnapShot.TransitionTo(snapshotFade);
   }

   [ProButton]
   public void StartPauseState()
   {
      pausedSnapShot.TransitionTo(snapshotFade);
   }

   [ProButton]
   public void StopPauseState()
   {
      mainSnapShot.TransitionTo(snapshotFade);
   }
}
