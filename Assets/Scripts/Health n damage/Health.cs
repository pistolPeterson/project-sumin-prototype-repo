
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> OnHealthChange; 
    [HideInInspector] public UnityEvent OnDeath; //common problem: making sure this event doesnt get spammed
     private int initialHealth = 50; //im scaling health x5 incase we need to balance 
    
    [Header("Debug Health")]
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool invincibleMode = false;
    [field: SerializeField] public int CurrentHealth { get; protected set; }
    [SerializeField] private float invincibilityDuration = 0.2f;

    public bool DebugInvincibilityMode { get; set; } = false;
    public virtual void Start()
    {
        //CurrentHealth = initialHealth;
    }


    public virtual void HandleDeath()
    {
        
    }
    
    public void Heal(int healthAmt)
    {
        
        CurrentHealth += healthAmt;
        if (CurrentHealth > initialHealth)
            CurrentHealth = initialHealth;
        OnHealthChange?.Invoke(healthAmt);
    }

    public void Damage(int damageAmt)
    {
        if(DebugInvincibilityMode)
            return;
        if(invincibleMode)
            return;
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmt, 0);
        OnHealthChange?.Invoke(-damageAmt);
        
        if (CurrentHealth == 0 && !isDead)
        {
            Debug.Log("We dead? ");
                HandleDeath();
                isDead = true;
                return;
        }
        StartCoroutine(InvincibilityFrameDuration());
    }
    private IEnumerator InvincibilityFrameDuration() {

        if (!invincibleMode) {
            invincibleMode = true;
            yield return new WaitForSeconds(invincibilityDuration);
        }
        invincibleMode = false;
    }
    public int GetInitialHealth() {
        return initialHealth;
    }
    public void AddMaxHealth(int newMaxHealth)
    {
        initialHealth += newMaxHealth;
    }
  
    public void SubtractMaxHealth(int newMaxHealth)
    {
        initialHealth -= newMaxHealth;
        if (initialHealth < 2)
            initialHealth = 2;
    }
    public void SetInvincibility(bool invincible) {
        invincibleMode = invincible;
    }

   
}
