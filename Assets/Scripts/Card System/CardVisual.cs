using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CardVisual : MonoBehaviour
{
   private Card parentCard;
   [SerializeField] private Transform shakeParent;
   [Header("Scale Parameters")]
   [SerializeField] private float scaleOnHover = 1.15f;
   [SerializeField] private float scaleOnSelect = 1.25f;
   [SerializeField] private float scaleTransitionTime = .15f;
   [SerializeField] private Ease scaleEase = Ease.OutBack;
   
   [Header("Select Parameters")]
   [SerializeField] private float selectPunchAmount = 20;
   
   [Header("Hover Parameters")]
   [SerializeField] private float hoverPunchAngle = 5;
   [SerializeField] private float hoverTransitionTime = .15f;

   [Header("Follow Parameters")]
   [SerializeField] private float followSpeed = 30;

   
   private Transform cardTransform;
   
   private float curveYOffset = 1f;
   public void Initialize(Card targetCard)
   {
      parentCard = targetCard;
      cardTransform = targetCard.transform;
      parentCard.OnCardBeginDrag.AddListener(BeginDrag);
      parentCard.OnCardEndDrag.AddListener(EndDrag);
      parentCard.OnCardSelected.AddListener(CardSelected);
   }

   private void Update()
   {
      SmoothFollow();
   }

   private void CardSelected(Card arg0, bool isCardSelected)
   {
     // DOTween.Kill(2, true);
     float dir = isCardSelected ? 1 : 0;
     shakeParent.DOPunchPosition(shakeParent.up * selectPunchAmount * dir, scaleTransitionTime, 10, 1);
     shakeParent.DOPunchRotation(Vector3.forward * (hoverPunchAngle/2), hoverTransitionTime, 20, 1).SetId(2);

        transform.DOScale(scaleOnHover, scaleTransitionTime).SetEase(scaleEase);
   }

   private void BeginDrag(Card card)
   {
      transform.DOScale(scaleOnSelect, scaleTransitionTime).SetEase(scaleEase);
   }
   
   private void EndDrag(Card card)
   {
      transform.DOScale(1, scaleTransitionTime).SetEase(scaleEase);
   }
   
   
   private void SmoothFollow()
   {
      Vector3 verticalOffset = (Vector3.up * (parentCard.IsDragging ? 0 : curveYOffset));
      transform.position = Vector3.Lerp(transform.position, cardTransform.position + verticalOffset, followSpeed * Time.deltaTime);
   }
}
