using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using MaskTransitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public InputAction pauseAction; 
    private PauseVisual pauseVisual;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenuButton;
    public PlayerControls playerControls;
    public List<Sprite> pauseResumeSprites;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        pauseVisual = GetComponent<PauseVisual>();
        playerControls = new PlayerControls();
        pauseButton.onClick.AddListener(() => TryPause(new InputAction.CallbackContext()));
        mainMenuButton.onClick.AddListener(GoMainMenu);
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        pauseAction.performed += TryPause;
    }

    private void OnEnable()
    {
        pauseAction = playerControls.Player.PauseAction;
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.performed -= TryPause;
    }

    private void TryPause(InputAction.CallbackContext obj)
    {
        Debug.Log("WE PAUSING BOYS");
        if(IsPaused())
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void GoMainMenu()
    {
        Debug.Log("was up");
        SceneManager.LoadScene(0);
        // TransitionManager.Instance.LoadLevel("MainfgafgafgMenu" );
    }

    [ProButton]
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseVisual.Show();
        pauseButton.GetComponent<Image>().sprite = pauseResumeSprites[1];
        playerMovement.IsActive = false;

    }
    [ProButton]
    public void ResumeGame()
    {
        
        Time.timeScale = 1;
        pauseVisual.Hide();
        pauseButton.GetComponent<Image>().sprite = pauseResumeSprites[0];
        playerMovement.IsActive = true;
    }

    public bool IsPaused() => Time.timeScale == 0;
}
