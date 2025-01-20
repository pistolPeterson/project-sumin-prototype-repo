using System;
using System.Collections;
using System.Collections.Generic;
using MaskTransitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CardSelectHandler : MonoBehaviour
{
    [SerializeField] private HorizontalCardHolder blessCardHolder;
    [SerializeField] private HorizontalCardHolder curseCardHolder;
    [SerializeField] private GameObject confirmButton;

    [HideInInspector] public UnityEvent OnPlayerConfirmedCard;
    private bool playerHasChosen = false;
    [SerializeField] private DialogueContainer introDc;
    private const float CHANCE_FOR_DIALOGUE = 0.5f;
    private bool dialoguePlayed = false;
    private void Start()
    {
        introDc.Play(introDc.GetRandomDialogue());
        blessCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        curseCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        ConfirmButtonVisual();
    }

    

    private void ConfirmButtonVisual()
    {
        bool bothCardsAreSelected = blessCardHolder.GetSelectedCard() && curseCardHolder.GetSelectedCard();
        confirmButton.SetActive(bothCardsAreSelected);
        if (!DialogueManager.Instance.IsActivelyPlaying && !dialoguePlayed)
        {
            if (CHANCE_FOR_DIALOGUE > Random.Range(0, 1f))
            {
                //introDc
                dialoguePlayed = true;
            }
        }
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
