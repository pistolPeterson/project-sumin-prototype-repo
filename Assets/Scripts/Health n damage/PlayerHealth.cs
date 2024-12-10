using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
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

    public void LoadHealth()
    {
        if (SaveManager.Instance.HasSave())
            CurrentHealth = SaveManager.Instance.CurrentSave.health;
        OnHealthChange.Invoke(0);
    }

    public void SaveHealth()
    {
        SaveManager.Instance.CurrentSave.health = CurrentHealth;
        SaveManager.Instance.SaveCurrent();
    }
}
