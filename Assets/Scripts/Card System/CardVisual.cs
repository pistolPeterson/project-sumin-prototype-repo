using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CardVisual : MonoBehaviour
{
   
   private Vector3 movementDelta;
   
   
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

   [Header("Rotation Parameters")]
   [SerializeField] private float rotationAmount = 20;
   [SerializeField] private float rotationSpeed = 20;
   private Vector3 rotationDelta;
   
   private Card parentCard;
   private Transform cardTransform;
   private Canvas canvas;
   
   private float curveYOffset = 0f; // not implemented yet
   public void Initialize(Card targetCard)
   {
      canvas = GetComponent<Canvas>();
      parentCard = targetCard;
      cardTransform = targetCard.transform;
      parentCard.OnCardBeginDrag.AddListener(BeginDrag);
      parentCard.OnCardEndDrag.AddListener(EndDrag);
      parentCard.OnCardSelected.AddListener(CardSelected);
      parentCard.OnCardPointerEnter.AddListener(PointerEnter);
      parentCard.OnCardPointerExit.AddListener(PointerExit);
      parentCard.OnCardPointerUp.AddListener(PointerUp);
      parentCard.OnCardPointerDown.AddListener(PointerDown);
   }

  


   private void Update()
   {
      SmoothFollow();
      FollowRotation();
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
      canvas.overrideSorting = true;//always make it show on top
   }
   
   private void EndDrag(Card card)
   {
      canvas.overrideSorting = false;

      transform.DOScale(1, scaleTransitionTime).SetEase(scaleEase);
   }

   private void PointerEnter(Card card)
   {
      transform.DOScale(scaleOnHover, scaleTransitionTime).SetEase(scaleEase);
      DOTween.Kill(2, true); //kill all tweens with ID of 2, but let them finish before it dies
      
      int vibrato = 20;
      float elasticity = 1f;
      shakeParent.DOPunchRotation(Vector3.forward * hoverPunchAngle, hoverTransitionTime, vibrato, elasticity).SetId(2);
   }

   private void PointerExit(Card card)
   {
      if (!parentCard.WasDragged)
         transform.DOScale(1, scaleTransitionTime).SetEase(scaleEase);
   }
   
   private void PointerUp(Card card, bool isLongPress)
   {
      Debug.Log("pointing up brub " + isLongPress);
      transform.DOScale(isLongPress ? scaleOnHover : scaleOnSelect, scaleTransitionTime).SetEase(scaleEase);
      canvas.overrideSorting = false;

   }
   
   private void PointerDown(Card card)
   {
      transform.DOScale(scaleOnSelect, scaleTransitionTime).SetEase(scaleEase);
   }
   
   private void SmoothFollow()
   {
      Vector3 verticalOffset = (Vector3.up * (parentCard.IsDragging ? 0 : curveYOffset));
      if (!parentCard.IsDragging)
         Debug.Log("vertical offset " + verticalOffset);
      transform.position = Vector3.Lerp(transform.position, cardTransform.position + verticalOffset, followSpeed * Time.deltaTime);
   }
   
   private void FollowRotation()
   {
      Vector3 movement = (transform.position - cardTransform.position);
      movementDelta = Vector3.Lerp(movementDelta, movement, 25 * Time.deltaTime);
      Vector3 movementRotation = (parentCard.IsDragging ? movementDelta : movement) * rotationAmount;
      rotationDelta = Vector3.Lerp(rotationDelta, movementRotation, rotationSpeed * Time.deltaTime);
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(rotationDelta.x, -60, 60));
   }
}
