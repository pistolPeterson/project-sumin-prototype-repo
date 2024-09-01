using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeVisual : MonoBehaviour
{
    private Material spriteMaterial;
    private const string GREYSCALE_MATERIAL_TAG = "_GreyscaleBlend";
    /*[SerializeField] Hi Raeus*/ private float transitionDuration = 0.15f;
    [SerializeField] private NodeMapButton nodeButton;
    private float visualDefaultSize = 1.5f;
    private float hoverSize = 2.5f;
        

    private void Awake()
    {
        nodeButton.OnNodePointerEnter.AddListener(VisualOnPointerEnter); 
        nodeButton.OnNodePointerExit.AddListener(VisualOnPointerExit);
        nodeButton.OnNodeSelected.AddListener(VisualOnSelected);
    }

    private void Start()
    {
        spriteMaterial = GetComponent<Renderer>().material;
        spriteMaterial.DOFloat(0, GREYSCALE_MATERIAL_TAG, transitionDuration );
       
    }

    private void VisualOnSelected()
    {
        transform.DOPunchScale(Vector3.one, transitionDuration, 10, 1);
    }
    
    //[ProButton]
    private void VisualOnPointerExit()
    {
       
       transform.DOScale(visualDefaultSize, transitionDuration);

    }

   // [ProButton]
    private void VisualOnPointerEnter()
    {
        transform.DOScale(hoverSize, transitionDuration);

    }

    public void ShowNodeVisualActive(bool isActive)
    {
        if (isActive)
            spriteMaterial.DOFloat(0, GREYSCALE_MATERIAL_TAG, transitionDuration);
        else
        {
            spriteMaterial.DOFloat(1, GREYSCALE_MATERIAL_TAG, transitionDuration);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill(false);
    }
}
