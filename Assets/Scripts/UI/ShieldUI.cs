using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Shield playerShield;
    [SerializeField] private Slider shieldBar;

    private void Start() {
        shieldBar.maxValue = playerShield.GetMaxShieldCharge();
        playerShield.OnShieldUse.AddListener(UpdateShieldUI);
    }
    private void UpdateShieldUI(float currentShield) {
        shieldBar.value = currentShield;
    }
}
