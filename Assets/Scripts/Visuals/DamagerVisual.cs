using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerVisual : MonoBehaviour
{
    [SerializeField] private Damager damager;
    [SerializeField] private Shake shake;
    void Start() {
        damager.OnDamage.AddListener(TriggerShake);
    }
    public void TriggerShake() {
        shake.TriggerShake();
    }

    
}
