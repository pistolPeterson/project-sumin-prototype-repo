using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVisual : MonoBehaviour
{
    private SpriteRenderer sr;
    private Shield shield;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        shield = GetComponentInParent<Shield>();
    }


    private void ShowShield()
    {
        
    }

    private void CloseShield()
    {
        
    }

    private void ShieldImpact()
    {
        
    }
}
