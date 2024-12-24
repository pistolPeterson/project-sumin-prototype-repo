using System;
using System.Collections;
using System.Collections.Generic;
using MaskTransitions;

using UnityEngine;
using UnityEngine.UI;

public class WinGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject gamePanelObj;
    [SerializeField] private Button mainMenuBttn;
    private void Start()
    {
        mainMenuBttn.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel("MainMenu");
        });
        HidePanel();
    }

    public void ShowPanel()
    {
        gamePanelObj.SetActive(true);
    }

    private void HidePanel()
    {
        gamePanelObj.SetActive(false);
    }
    
    
}