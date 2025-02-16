using System;
using System.Collections;
using System.Collections.Generic;
using MaskTransitions;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CardSelectHandler : MonoBehaviour
{
    [SerializeField] private HorizontalCardHolder blessCardHolder;
    [SerializeField] private HorizontalCardHolder curseCardHolder;
    [SerializeField] private GameObject confirmButton;

    [HideInInspector] public UnityEvent OnPlayerConfirmedCard;
    private bool playerHasChosen = false;
    [SerializeField] private DialogueContainer introDc;
    [SerializeField] private DialogueContainer confirmCardDc;
   [SerializeField] private DialogueContainer pickCardDc;

    private const float CHANCE_FOR_DIALOGUE = 0.4f;
    private bool dialoguePlayed = false;

    private void Start()
    {
        StartCoroutine(PeteUtility.WaitThenCall(introDc.Play, 0.75f));
        blessCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        curseCardHolder.OnAnyCardSelected.AddListener(ConfirmButtonVisual);
        confirmButton.SetActive(false);
    }


    private void ConfirmButtonVisual()
    {
        bool bothCardsAreSelected = blessCardHolder.GetSelectedCard() && curseCardHolder.GetSelectedCard();
        confirmButton.SetActive(bothCardsAreSelected);
        if (!DialogueManager.Instance.IsActivelyPlaying && !dialoguePlayed)
        {
            if (CHANCE_FOR_DIALOGUE > Random.Range(0, 1f))
            {
                //player picks card dialogue
                pickCardDc.Play();
                dialoguePlayed = true;
            }
        }
    }

    public void OnConfirmCards()
    {
        if (playerHasChosen)
            return;
        if (!blessCardHolder.GetSelectedCard() || !curseCardHolder.GetSelectedCard())
        {
            Debug.LogError("did you select both cards buddy? ");
            return;
        }
        //STOP PLAYER INTERACTION
        DisableCardInteraction();
        confirmCardDc.Play();
        //TODO: fix these references
        var blessCardSO = blessCardHolder.GetSelectedCard().GetCardVisual().gameObject.GetComponent<CardSOVisual>()
            .cardSo;
        var curseCardSO = curseCardHolder.GetSelectedCard().GetCardVisual().gameObject.GetComponent<CardSOVisual>()
            .cardSo;


        SaveManager.Instance.CurrentSave.playerCards.Add(blessCardSO);
        SaveManager.Instance.CurrentSave.playerCards.Add(curseCardSO);
        OnPlayerConfirmedCard?.Invoke();
        TransitionManager.Instance.LoadLevel("Scenes/Gameplay Scenes/RealNodeMap", 2.5f);
        playerHasChosen = true;
    }

    private void DisableCardInteraction()
    {
        
        var cards = FindObjectsOfType<CardVisual>();
        foreach (var card in cards)
        {
            card.SetCardFinal();
        }
    }
}