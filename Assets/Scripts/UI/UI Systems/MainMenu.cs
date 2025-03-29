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
        ambienceSystem.StopAmbienceSystem();
        FindObjectOfType<PulseAlphaEffect>()?.KillAnim();
        SaveManager.Instance.CreateNewSave();
        TransitionManager.Instance.LoadLevel("RealNodeMap");
    }

    public void OnContinueGameClicked()
    {        
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
    }

    public void OpenSettings()
    {
        CloseAllGroups();
        settingsGroup.SetActive(true);
        backButton.SetActive(true);
    }

    public void OpenCredits()
    {
        CloseAllGroups();
        creditsGroup.SetActive(true);
        backButton.SetActive(true);
    }

    private void CloseAllGroups()
    {
        mainMenuGroup.SetActive(false);
        settingsGroup.SetActive(false);
        creditsGroup.SetActive(false);
    }
}