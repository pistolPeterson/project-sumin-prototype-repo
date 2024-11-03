using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IDataPersist
{
    [SerializeField] private ParticleSystem deathPS;
    public override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        //this is the default health being set, if save is on, it will be overriden OnEnable 
        CurrentHealth = GetInitialHealth();
    }

    public override void HandleDeath()
    {
        deathPS.Play();
        IEnumerator DelayToDie() {
            yield return new WaitForSeconds(deathPS.main.duration / 2);
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        }
        StartCoroutine(DelayToDie());
    }
    
    
    public void LoadData(GameData data)
    {
        CurrentHealth = data.currentHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = CurrentHealth;
    }
}
