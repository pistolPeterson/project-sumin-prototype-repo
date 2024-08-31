using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private ParticleSystem deathPS;
    public override void Start()
    {
        base.Start();
    }

 
    public override void HandleDeath()
    {
        Debug.Log("Player is dead af");
        deathPS.Play();
        IEnumerator DelayToDie() {
            yield return new WaitForSeconds(deathPS.main.duration / 2);
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        }
        StartCoroutine(DelayToDie());
    }
}
