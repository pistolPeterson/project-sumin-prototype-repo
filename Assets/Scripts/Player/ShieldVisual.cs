using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ShieldVisual : MonoBehaviour
{
    private Shield shield;

     [SerializeField] private GameObject shieldVisualGO; 

  
    private void ShowShield()
    {
        shieldVisualGO.SetActive(true);
    }
    private void CloseShield()
    {
        shieldVisualGO.SetActive(false);
    }

}
