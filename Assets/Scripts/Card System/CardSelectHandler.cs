using System;
using System.Collections;
using System.Collections.Generic;
using MaskTransitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CardSelectHandler : MonoBehaviour
{
    [SerializeField] private HorizontalCardHolder blessCardHolder;
    [SerializeField] private HorizontalCardHolder curseCardHolder;
    [SerializeField] private GameObject confirmButton;

    [HideInInspector] public UnityEvent OnPlayerConfirmedCard;
    private bool playerHasChosen = false;
    //connect with button UI on click
    //TODO: 

    private void Start()
    {
        blessCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        curseCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        ConfirmButtonVisual();
    }

    

    private void ConfirmButtonVisual()
    {
        bool bothCardsAreSelected = blessCardHolder.GetSelectedCard() && curseCardHolder.GetSelectedCard();
        confirmButton.SetActive(bothCardsAreSelected);
    }
    public void OnConfirmCards()
    {
        if(playerHasChosen)
            return;
        if(!blessCardHolder.GetSelectedCard() || !curseCardHolder.GetSelectedCard())
        {
            Debug.LogError("did you select both cards buddy? ");
            return;
        }

        //from these 2 selected cards, get their SO's 
        //TODO: fix these references
        var blessCardSO = blessCardHolder.GetSelectedCard().GetCardVisual().gameObject.GetComponent<CardSOVisual>().cardSo;
        var curseCardSO = curseCardHolder.GetSelectedCard().GetCardVisual().gameObject.GetComponent<CardSOVisual>().cardSo;
        //send to gamemanager 
       // GameManager.Instance.currentPlayerHand.Add(blessCardSO);
        //GameManager.Instance.currentPlayerHand.Add(curseCardSO);
        
        SaveManager.Instance.CurrentSave.playerCards.Add(blessCardSO);
        SaveManager.Instance.CurrentSave.playerCards.Add(curseCardSO);
        OnPlayerConfirmedCard?.Invoke();
        TransitionManager.Instance.LoadLevel("Scenes/Gameplay Scenes/RealNodeMap", 2.5f);
        playerHasChosen = true;
    }

  

}
