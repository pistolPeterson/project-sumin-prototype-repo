using System.Collections;
using System.Collections.Generic;
using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGroup;
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private GameObject creditsGroup;
    [SerializeField] private GameObject backButton;
    
    //TODO: if there is no game data, disable continue button
    private void Start() {
        OpenMainMenu();
    }
    public void StartGame() {
       
        //when mainmenu audio finishes, game goes to next scene  
    }

    public void OnContinueGameClicked()
    {
        TransitionManager.Instance.LoadLevel("RealNodeMap");
    }

    public void GoToNextScene() //called by audio system when its audio is done 
    {
      FindObjectOfType<PulseAlphaEffect>()?.KillAnim();
       SaveManager.Instance.CreateNewSave();
       TransitionManager.Instance.LoadLevel("RealNodeMap");
    }
    public void OpenMainMenu() {
        CloseAllGroups();
        mainMenuGroup.SetActive(true);
        backButton.SetActive(false);
    }
    public void OpenSettings() {
        CloseAllGroups();
        settingsGroup.SetActive(true);
        backButton.SetActive(true);
    }
    public void OpenCredits() {
        CloseAllGroups();
        creditsGroup.SetActive(true);
        backButton.SetActive(true);
    }
    private void CloseAllGroups() {
        mainMenuGroup.SetActive(false);
        settingsGroup.SetActive(false);
        creditsGroup.SetActive(false);
    }
}
