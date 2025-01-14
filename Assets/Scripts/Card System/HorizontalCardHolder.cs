using System;
using System.Collections;
using System.Collections.Generic;
using PeteUnityUtils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalCardHolder : MonoBehaviour
{
    public List<Card> cardsInHand;
    public List<CardDataBaseSO> cardsDataInHand;

    [Header("Debug Card Status")] [SerializeField]
    private Card selectedCard;

    [SerializeField] private bool isBlessCards;
    [SerializeField] private int amountOfCards = 3;
    [SerializeField] private GameObject cardBasePrefab;
    public UnityEvent OnAnyCardSelected;
    private void Awake()
    {
        //another script has a global list of all the cards 
    }


    private void Start()
    {
        //TODO: get ALLCards reference
        var cardPool = FindObjectOfType<AllCards>().CardPool;
        cardPool.Shuffle();
        //get all the bless/curse cards and add to hand
        int currentAmt = 0;
        foreach (var card in cardPool)
        {
            if (isBlessCards && card.GetCardType() == CardFateType.BlessCard)
            {
                cardsDataInHand.Add(card);
                currentAmt++;
            }

            if (!isBlessCards && card.GetCardType() == CardFateType.CurseCard)
            {
                cardsDataInHand.Add(card);
                currentAmt++;
            }

            if (currentAmt >= amountOfCards)
                break;
        }

        //spawn the cards in cardsInHand, and set the visuals
        for (int i = 0; i < amountOfCards; i++)
        {
            var cardGO = Instantiate(cardBasePrefab, this.transform);
            var card = cardGO.GetComponentInChildren<Card>();
            if (card.GetCardVisual() == null)
            {
                Debug.Log("card visual is null bro");
            }

            SetUpListener(card);
            var cardSOVisual = card.GetCardVisual().gameObject.GetComponent<CardSOVisual>();
            cardSOVisual.cardSo = cardsDataInHand[i];
            cardSOVisual.UpdateCardVisual();
        }
    }

    private void SetUpListener(Card card)
    {
        card.OnCardBeginDrag.AddListener(OnBeginDrag);
        card.OnCardEndDrag.AddListener(OnEndDrag);
        card.OnCardSelected.AddListener(_OnCardSelected);
    }

    private void _OnCardSelected(Card card, bool isCardSelected)
    {
        if (isCardSelected)
        {
            selectedCard?.DeselectCard();
            selectedCard = card;
        }
        else
        {
            selectedCard = null;
        }
        OnAnyCardSelected?.Invoke();
    }

    public void OnBeginDrag(Card card)
    {
        selectedCard = card;
    }

    public void OnEndDrag(Card card)
    {
        //test code
        selectedCard.transform.localPosition = new Vector3(0, 0, 0);

        selectedCard = null;
    }

    public Card GetSelectedCard() => selectedCard;
}