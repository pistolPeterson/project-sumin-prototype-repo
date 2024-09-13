using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGroup;
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private GameObject creditsGroup;
    [SerializeField] private GameObject backButton;
    private void Start() {
        OpenMainMenu();
    }
    public void StartGame() {
        Debug.Log("Game STARTO!"); //ok raeus.
        //when mainmenu audio finishes, game goes to next scene  
    }

    public void GoToNextScene() //called by audio system when its audio is done 
    {
        SceneManager.LoadScene(1);
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
