using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Shield playerShield;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private Slider shieldBar;

    private void Start() {
        playerShield.OnShieldUse.AddListener(UpdateShieldUI);
    }
    private void UpdateShieldUI(float currentShield) {
        shieldText.text = "Shield: " + currentShield;
        shieldBar.value = currentShield;
    }
}
