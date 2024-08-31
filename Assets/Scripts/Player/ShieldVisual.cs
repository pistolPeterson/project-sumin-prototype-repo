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
    //[Header("mind you business rae")]
    //public Color[] colorPallete; 
    private int colorPalleteIndex = 1; //skipping one since its same color as background
    private float colorChangeTime = 0.125f;

    [SerializeField] private GameObject shieldVisual; 

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        shield = GetComponentInParent<Shield>();
    }
    private void Update() {
        if (shield.GetUsingShield() && shield.GetCanUseShield()) {
            ShowShield();
        }
        else CloseShield();
    }
    /*private void ShieldSpawn() //shield showcase at the begining of the round, just for some cool points. can be 
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
      
        sr.DOColor(colorPallete[colorPalleteIndex], colorChangeTime).OnComplete(NextColor);
        
    }*/
    private void ShowShield()
    {
        shieldVisual.SetActive(true);
    }
    private void CloseShield()
    {
        shieldVisual.SetActive(false);
    }

}
