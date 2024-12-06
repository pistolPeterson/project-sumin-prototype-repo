using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenuAudio : MonoBehaviour, IDataPersist
{
    [Header("Main Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    [Header("Audio")]
    [SerializeField] private AudioSource mainMenuUIAudioSource;
    public List<AudioClip> uiAudioClips;

    [Header("Audio Sliders")] 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private AudioMixer mixer;

    private const string MUSIC_LVL_KEY = "MUSIC_LEVEL";
    private const string SFX_LVL_KEY = "SFX_LEVEL";

    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonAudio);
        settingsButton.onClick.AddListener(SettingsButtonAudio);
        creditsButton.onClick.AddListener(CreditsButtonAudio);
        backButton.onClick.AddListener(BackButtonAudio);
        
        musicSlider.onValueChanged.AddListener(SetMusicLevel);
        sfxSlider.onValueChanged.AddListener(SetSFXLevel);

        /*
            A system setting like this doesn't make sense to be in the save file,
            so it is saved in the player preferences instead.
        */
        musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MUSIC_LVL_KEY, 1));
        SetMusicLevel(musicSlider.value);

        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(SFX_LVL_KEY, 1));
        SetSFXLevel(sfxSlider.value);
    }

    private void SetMusicLevel(float newValue)
    {
        SetSliderVolume(musicSlider, musicText, "MusicParam");
        PlayerPrefs.SetFloat(MUSIC_LVL_KEY, newValue);
    }
    
    private void SetSFXLevel(float newValue)
    {
        SetSliderVolume(sfxSlider, sfxText, "SFXParam");
        PlayerPrefs.SetFloat(SFX_LVL_KEY, newValue);
    }
   
    private void PlayButtonAudio()
    {
        mainMenuUIAudioSource.PlayOneShot(uiAudioClips[0]);
        mainMenuUIAudioSource.PlayOneShot(uiAudioClips[4]);
        var lengthOfAudioClip = Mathf.Max(uiAudioClips[0].length, uiAudioClips[4].length);
        StartCoroutine(WaitThenGoToNextScene());
        IEnumerator WaitThenGoToNextScene()
        {
            yield return new WaitForSeconds(lengthOfAudioClip);
            FindObjectOfType<MainMenu>()?.GoToNextScene();
        }
    }
    
    private void BackButtonAudio()
    {
        mainMenuUIAudioSource.PlayOneShot(uiAudioClips[1]);
    }
    private void SettingsButtonAudio()
    {
        mainMenuUIAudioSource.PlayOneShot(uiAudioClips[3]);
    }
    
    private void CreditsButtonAudio()
    {
        mainMenuUIAudioSource.PlayOneShot(uiAudioClips[3]);
    }

    private void SetSliderVolume(Slider slider, TextMeshProUGUI text, string mixerParam)
    {
        mixer.SetFloat(mixerParam, Mathf.Log10(slider.value) * 20);
        text.text = (slider.value * 100).ToString();
    }

    public void LoadData(GameData data)
    {
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
    }

    public void SaveData(ref GameData data)
    {
        data.musicVolume = musicSlider.value;
        data.sfxVolume = sfxSlider.value;
    }
}
