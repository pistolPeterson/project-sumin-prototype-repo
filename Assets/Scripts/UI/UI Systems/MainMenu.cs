using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGroup;
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private GameObject creditsGroup;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private AmbienceSystem ambienceSystem;
    private bool onPlayFirstTime = false;
    private void Start()
    {
        OpenMainMenu();
        StartCoroutine(WaitThenUpdateButton());

        IEnumerator WaitThenUpdateButton()
        {
            yield return new WaitForSeconds(0.25f);
            UpdateContinueButtonStatus();
        }
    }

    public void OnNewGameClicked()
    {
        var clip= AudioSOHandler.Instance.MainMenuAudioSO.NewGameOrContinueGameSFXClip;
        AudioSOHandler.Instance.PlayOneShot(clip);
        ambienceSystem.StopAmbienceSystem();
        FindObjectOfType<PulseAlphaEffect>()?.KillAnim();
        SaveManager.Instance.CreateNewSave();
        TransitionManager.Instance.LoadLevel("RealNodeMap");
    }

    public void OnContinueGameClicked()
    {      
        var clip= AudioSOHandler.Instance.MainMenuAudioSO.NewGameOrContinueGameSFXClip;
        AudioSOHandler.Instance.PlayOneShot(clip);
        ambienceSystem.StopAmbienceSystem();
        SaveManager.Instance.LoadAllDataOnline();
        TransitionManager.Instance.LoadLevel("RealNodeMap");
    }

    [ProButton]
    public async void UpdateContinueButtonStatus()
    {
        var playerHasSaveData = await SaveManager.Instance.HasSave();
        continueButton.SetActive(playerHasSaveData);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void OpenMainMenu()
    {
        CloseAllGroups();
        mainMenuGroup.SetActive(true);
        backButton.SetActive(false);
        
        PlayBackBttnSFX();
    }

    public void OpenSettings()
    {
        CloseAllGroups();
        settingsGroup.SetActive(true);
        backButton.SetActive(true);
     
        PlayPanelsSFX();
    }

    private void PlayBackBttnSFX()
    {
        if (!onPlayFirstTime)
        {    
            onPlayFirstTime = true;
           
        }
        else
        {
            var clip= AudioSOHandler.Instance.MainMenuAudioSO.CreditsOrSettingsBttnSFX;
            AudioSOHandler.Instance.PlayOneShot(clip);
        }

    }

    public void PlayBttnHoverSFX()
    {
        var clip= AudioSOHandler.Instance.MainMenuAudioSO.ButtonHoverSFX; 
        AudioSOHandler.Instance.PlayOneShot(clip);
    }
    public void PlayPanelsSFX()
    {
        var clip= AudioSOHandler.Instance.MainMenuAudioSO.CreditsOrSettingsBttnSFX; //I prefer back bttn sound than credits/settings sound 
        AudioSOHandler.Instance.PlayOneShot(clip);
    }
    public void OpenCredits()
    {
        CloseAllGroups();
        creditsGroup.SetActive(true);
        backButton.SetActive(true);
        PlayPanelsSFX();
    }

    private void CloseAllGroups()
    {
        mainMenuGroup.SetActive(false);
        settingsGroup.SetActive(false);
        creditsGroup.SetActive(false);
    }
}