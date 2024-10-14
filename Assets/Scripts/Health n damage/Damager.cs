using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour //simple class that hurts player on contact
{
    [field: SerializeField] public int CurrentDamage { get; set; } = 2;
    [HideInInspector] public UnityEvent OnDamage;

   
    
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.Damage(CurrentDamage);
            OnDamage.Invoke();
        }
    }
}
