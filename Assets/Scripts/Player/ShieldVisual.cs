using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

public class ShieldVisual : MonoBehaviour
{
    private SpriteRenderer sr;
    private Shield shield;
    [Header("mind you business rae")]
    public Color[] colorPallete; 
    private int colorPalleteIndex = 1; //skipping one since its same color as background
    private float colorChangeTime = 0.125f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        shield = GetComponentInParent<Shield>();
        ShieldSpawn();
    }


    [ProButton]
    private void ShieldSpawn() //shield showcase at the begining of the round, just for some cool points. can be 
    {
        //go through all colors of pallete...
        sr.DOColor(colorPallete[colorPalleteIndex], colorChangeTime).OnComplete(NextColor);
        //whiles spinning...
        //and getting bigger...
    }

    private void NextColor()
    {
        colorPalleteIndex++;
        if(colorPalleteIndex >= colorPallete.Length)
            return;
        float punchForce = 2.5f;
        //transform.DOPunchScale(Vector3.one * punchForce, colorChangeTime, 5, 1f);
        sr.DOColor(colorPallete[colorPalleteIndex], colorChangeTime).OnComplete(NextColor);
        
    }
    
    [ProButton]
    private void ShowShield()
    {
        
    }

    [ProButton]
    private void CloseShield()
    {
        
    }
    [ProButton]
    private void ShieldImpact()
    {
        
    }
}
