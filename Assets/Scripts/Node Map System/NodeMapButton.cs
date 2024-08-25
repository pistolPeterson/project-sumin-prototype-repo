using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeMapButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button button;

    [HideInInspector] public UnityEvent OnNodePointerEnter;
    [HideInInspector] public UnityEvent OnNodePointerExit;
    [HideInInspector] public UnityEvent OnNodeSelected;
    private INode node;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        node = GetComponentInParent<INode>();
if(node == null)
    Debug.LogError("Cant find an INode instance in the parent");
    }

    private void OnButtonClick()
    {
        node.OnNodeInteract();
        OnNodeSelected?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnNodePointerExit?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnNodePointerEnter?.Invoke();
    }
}