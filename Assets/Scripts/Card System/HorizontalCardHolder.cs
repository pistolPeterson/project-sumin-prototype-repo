using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
   public List<Card> cardsInHand;
   public List<CardDataBaseSO> cardsDataInHand;
   [Header("Debug Card Status")] 
   [SerializeField] private Card selectedCard;

   [SerializeField] private bool isBlessCards; 
   
   private void Awake()
   {
      SetUpListeners();
      //another script has a global list of all the cards 
      var cardPool = FindObjectOfType<AllCards>().CardPool;
      //get all the bless/curse cards and add to hand
      foreach (var card in cardPool)
      {
         if (isBlessCards)
         {
            if (card is BlessCardBase)
            {
               cardsDataInHand.Add(card);
            }
         }
      }
      //wtf
      
   }

   private void SetUpListeners()
   {
      foreach (var card in cardsInHand)
      {
         card.OnCardBeginDrag.AddListener(OnBeginDrag);
         card.OnCardEndDrag.AddListener(OnEndDrag);
         card.OnCardSelected.AddListener(_OnCardSelected);
      }
   }

   private void _OnCardSelected(Card card, bool isCardSelected)
   {
      
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
}
