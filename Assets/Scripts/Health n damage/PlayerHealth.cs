using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IDataPersist
{
    [SerializeField] private ParticleSystem deathPS;
    [SerializeField] private GameObject playerVisual;
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
        StartCoroutine(DelayToDie());
    }
    
    IEnumerator DelayToDie() {
        yield return new WaitForSeconds(deathPS.main.duration / 2);
        playerVisual.SetActive(false);
        OnDeath?.Invoke();
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
