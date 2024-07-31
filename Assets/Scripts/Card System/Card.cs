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
    private Canvas canvas;
    public bool IsDragging { get; private set; }
    [Header("Movement")]
    [SerializeField] [Range(30f, 70f)]private float moveSpeedLimit = 50f;

    [Header("Selection Data")] 
    [SerializeField] [Range(10f, 40f)] private float selectedOffset = 25f;
    [field: SerializeField] public bool IsSelected { get; set; }
   
    [Header("Card Events")] 
    [HideInInspector] public UnityEvent<Card> OnCardBeginDrag;
    [HideInInspector] public UnityEvent<Card> OnCardEndDrag;
    [HideInInspector] public UnityEvent<Card, bool> OnCardSelected;

    private CardVisual cardVisual;
    [SerializeField] private GameObject cardVisualPrefab;
    private VisualCardHandler visualHandler;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        visualHandler = FindObjectOfType<VisualCardHandler>();

    }

    private void Start()
    {
        cardVisual = Instantiate(cardVisualPrefab, visualHandler ? visualHandler.transform : canvas.transform).GetComponent<CardVisual>();
        cardVisual.Initialize(this);
        IsSelected = false;
    }

    private void Update()
    {
        ClampPosition();
        if (IsDragging)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)- offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        IsDragging = true;
        OnCardBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
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
