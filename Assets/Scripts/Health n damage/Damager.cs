using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour //simple class that hurts player on contact
{
    private int initialDamageAmt = 2; 
    [field: SerializeField] public int currentDamage { get; private set; }
    [HideInInspector] public UnityEvent OnDamage;

    private void Start()
    {
        currentDamage = initialDamageAmt;
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.Damage(currentDamage);
            OnDamage.Invoke();
        }
    }
}
