using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
   public List<Card> cardsInHand;

   [Header("debug card status")] 
   [SerializeField] private Card selectedCard;

   private void Awake()
   {
      SetUpListeners();
   }

   private void SetUpListeners()
   {
      foreach (var card in cardsInHand)
      {
         card.OnCardBeginDrag.AddListener(OnBeginDrag);
         card.OnCardEndDrag.AddListener(OnEndDrag);
      }
   }

   public void OnBeginDrag(Card card)
   {
      selectedCard = card;
   }
   
   public void OnEndDrag(Card card)
   {
      selectedCard = null;
   }
}
