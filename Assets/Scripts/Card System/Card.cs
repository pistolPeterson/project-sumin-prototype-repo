using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, 
    IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector3 offset;
    private Canvas canvas;
    public bool IsDragging { get; private set; }
    public bool WasDragged { get; private set; }
    [Header("Movement")]
    [SerializeField] [Range(30f, 70f)]private float moveSpeedLimit = 50f;

    [Header("Selection Data")] 
    [SerializeField] [Range(10f, 40f)] private float selectedOffset = 25f;
    private float pointerDownTime; //time values to check for long press
    private float pointerUpTime;
    [field: SerializeField] public bool IsSelected { get; set; }
   
    [Header("Card Events")] 
    [HideInInspector] public UnityEvent<Card> OnCardBeginDrag;
    [HideInInspector] public UnityEvent<Card> OnCardEndDrag;
    [HideInInspector] public UnityEvent<Card, bool> OnCardSelected;
    [HideInInspector] public UnityEvent<Card> OnCardPointerEnter;
    [HideInInspector] public UnityEvent<Card> OnCardPointerExit;
    [HideInInspector] public UnityEvent<Card, bool> OnCardPointerUp;
    [HideInInspector] public UnityEvent<Card> OnCardPointerDown;
    private CardVisual cardVisual;
    [SerializeField] private GameObject cardVisualPrefab;
    private VisualCardHandler visualHandler;
   [SerializeField] private ParticleSystem cardParticleSystem;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        visualHandler = FindObjectOfType<VisualCardHandler>();
        cardVisual = Instantiate(cardVisualPrefab, visualHandler ? visualHandler.transform : canvas.transform).GetComponent<CardVisual>();
        cardVisual.Initialize(this);

    }

    private void Start()
    {
       
        IsSelected = false;
    }

    public void ShowCardSelectedVFX(bool bub)
    {
        if(!cardParticleSystem)
            cardParticleSystem = cardVisual.gameObject.GetComponentInChildren<ParticleSystem>();
        cardParticleSystem.gameObject.SetActive(bub);
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
      //sfx?
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        IsDragging = true;
        OnCardBeginDrag?.Invoke(this);
        WasDragged = true;
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        OnCardEndDrag?.Invoke(this);
        
        StartCoroutine(FrameWait()); 

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            WasDragged = false; 
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnCardPointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnCardPointerExit?.Invoke(this);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUpTime = Time.time;

        float longPressTime = .2f;
        bool isLongPress = pointerUpTime - pointerDownTime > longPressTime;
        OnCardPointerUp?.Invoke(this, isLongPress);
        
        if (isLongPress)
            return;
        
        if(WasDragged)
            return;

        
        IsSelected = !IsSelected;
        OnCardSelected?.Invoke(this, IsSelected);
     

        if (IsSelected)
        {
            transform.localPosition += transform.up * selectedOffset;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }

    }

    public void DeselectCard()
    {
        transform.localPosition = Vector3.zero;
        IsSelected = false;
        ShowCardSelectedVFX(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnCardPointerDown.Invoke(this);
        pointerDownTime = Time.time;
    }

    public CardVisual GetCardVisual() => cardVisual;
}
