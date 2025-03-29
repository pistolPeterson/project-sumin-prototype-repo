using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAudioBus : MonoBehaviour
{
    string sfxBusString = "Bus:/SFX";
    string musicBusString = "Bus:/Music";
    FMOD.Studio.Bus sfxBus;
    FMOD.Studio.Bus musicBus;
    private bool musicToggleState = true;
    private bool sfxToggleState = true;

    [SerializeField] private Button sfxButton; 
    [SerializeField] private Button musicButton;

    private void Awake()
    {
        sfxButton.onClick.AddListener(ToggleSFX);
        musicButton.onClick.AddListener(ToggleMusic);
    }

    private void Start(){

        sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
        sfxBus.setMute(false);
        
        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.setMute(false);
       
    }


    public void ToggleMusic()
    {
        musicToggleState = !musicToggleState;
        musicBus.setMute(musicToggleState);
    }
    
    public void ToggleSFX()
    {
        sfxToggleState = !sfxToggleState;
        sfxBus.setMute(sfxToggleState);
    }

   
}
