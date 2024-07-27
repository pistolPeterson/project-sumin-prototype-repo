using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, 
    IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector3 offset;

    private bool isDragging = false;
    [Header("Movement")]
    [SerializeField] [Range(25f, 75f)]private float moveSpeedLimit = 50f;

    [Header("Selection Data")] 
    [SerializeField] [Range(10f, 250f)] private float selectedOffset = 125f;
    [field: SerializeField] public bool IsSelected { get; set; }
   
    [Header("Card Events")] 
    [HideInInspector] public UnityEvent<Card> OnCardBeginDrag;
    [HideInInspector] public UnityEvent<Card> OnCardEndDrag;
    [HideInInspector] public UnityEvent<Card, bool> OnCardSelected;


    private void Start()
    {
        IsSelected = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)- offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        isDragging = true;
        OnCardBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        OnCardEndDrag?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsSelected = !IsSelected;
        OnCardSelected?.Invoke(this, IsSelected);

        if (IsSelected)
            transform.localPosition += transform.up * selectedOffset;
        else
            transform.localPosition = Vector3.zero;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
