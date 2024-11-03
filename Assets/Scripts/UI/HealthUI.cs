using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Microlight.MicroBar;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
   private MicroBar healthBar;
   private PlayerHealth playerHealth;
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
   }

   private void Start()
   {
      float maxHp = (float)playerHealth.GetInitialHealth();
      healthBar.Initialize(maxHp);
   }


}
