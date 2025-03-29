using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class TestScript : MonoBehaviour
{
   public EventReference musicRef;
   public EventReference sfxRef;
   public EventReference uiRef;
   public EventReference dialogueRef;
   private FMOD.Studio.EventInstance musicInstance;


   private void Start()
   {
      
      //start music instance
      musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicRef);
   }

   [ProButton]
   private void PlayMusic()
   {
      musicInstance.start();
   }

   [ProButton]
   private void StopMusic()
   {
      musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
   }

   [ProButton]
   private void PlayDialogue()
   {
      FMODUnity.RuntimeManager.PlayOneShot(dialogueRef, transform.position);
   }
   
   [ProButton]
   private void PlaySFX()
   {
      FMODUnity.RuntimeManager.PlayOneShot(sfxRef, transform.position);
   }

   [ProButton]
   private void PlayUI()
   {
      FMODUnity.RuntimeManager.PlayOneShot(uiRef, transform.position);
   }
}
