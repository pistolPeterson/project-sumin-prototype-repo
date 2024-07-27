using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
   public List<Card> cardsInHand;

   [Header("Debug Card Status")] 
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
