using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenuAudio : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;

    [Header("Audio Sliders")] 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;

    private string musicBusKey = "bus:/Music";
    private string sfxBusKey = "bus:/SFX";
    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus sfxBus;
    

    private const string MUSIC_LVL_KEY = "MUSIC_LEVEL";
    private const string SFX_LVL_KEY = "SFX_LEVEL";

    private bool sfxChange = false;
    private float musicChangeTime = 5.0f;
    [SerializeField] private DialogueContainer dc;
    private bool dcHasBeenCalled = false;
    private void Start()
    {
        musicBus = RuntimeManager.GetBus(musicBusKey);
        sfxBus = RuntimeManager.GetBus(sfxBusKey);
        
        settingsButton.onClick.AddListener(SettingsButtonAudio);
        creditsButton.onClick.AddListener(CreditsButtonAudio);
        backButton.onClick.AddListener(BackButtonAudio);
        
        musicSlider.onValueChanged.AddListener(SetMusicLevel);
        sfxSlider.onValueChanged.AddListener(SetSFXLevel);

    
        musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MUSIC_LVL_KEY, 1));
        SetMusicLevel(musicSlider.value);

        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(SFX_LVL_KEY, 1));
        SetSFXLevel(sfxSlider.value);
    }

    private void SetMusicLevel(float newValue)
    {
        SetSliderVolume(musicSlider, musicText, "MusicParam");
        musicBus.setVolume(musicSlider.value / musicSlider.maxValue);
        PlayerPrefs.SetFloat(MUSIC_LVL_KEY, newValue);
    
    }

    private IEnumerator PlayTestAudio()
    {
        sfxChange = true;
        var dlg = dc.GetRandomDialogue();
        dc.Play();
        yield return new WaitForSeconds( dlg.GetAudioLength());
        sfxChange = false;
    }

    private void SetSFXLevel(float newValue)
    {
        SetSliderVolume(sfxSlider, sfxText, "SFXParam");
        sfxBus.setVolume(sfxSlider.value / sfxSlider.maxValue);
        PlayerPrefs.SetFloat(SFX_LVL_KEY, newValue);
        if (!dcHasBeenCalled)
        {
            dcHasBeenCalled = true;
            return;
        }
        if (!sfxChange)
        {
           // StartCoroutine( PlayTestAudio());
        }
    }
   

    
    private void BackButtonAudio()
    {
        //mainMenuUIAudioSource.PlayOneShot(uiAudioClips[1]);
    }
    private void SettingsButtonAudio()
    {
       // mainMenuUIAudioSource.PlayOneShot(uiAudioClips[3]);
    }
    
    private void CreditsButtonAudio()
    {
       // mainMenuUIAudioSource.PlayOneShot(uiAudioClips[3]);
    }

    private void SetSliderVolume(Slider slider, TextMeshProUGUI text, string mixerParam)
    {
     //   mixer.SetFloat(mixerParam, Mathf.Log10(slider.value) * 20);
        text.text = ((int)(slider.value * 100)).ToString();
    }


}
