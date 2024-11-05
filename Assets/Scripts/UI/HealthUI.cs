using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Microlight.MicroBar;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
   private MicroBar healthBar;
   private PlayerHealth playerHealth;
   [SerializeField] private TextMeshProUGUI healthText;
   private float maxHp = 0;
   private void Awake()
   {
      healthBar = GetComponent<MicroBar>();
      playerHealth = FindObjectOfType<PlayerHealth>();
      playerHealth.OnHealthChange.AddListener(UpdateBarHealth);
   }

   private void UpdateBarHealth(int healthChangeAmt)
   {
      float currentHealth = playerHealth.CurrentHealth;
      healthBar.UpdateBar(currentHealth);
      healthText.text = $"{(int)currentHealth} / {(int)maxHp}";
   }

   private void Start()
   {
       maxHp = (float)playerHealth.GetInitialHealth();
      healthBar.Initialize(maxHp);
      UpdateBarHealth(0);
   }


}
