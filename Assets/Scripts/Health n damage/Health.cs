
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public UnityEvent<bool> OnHealthChange; //bool is if its a positive health change
    [HideInInspector] public UnityEvent OnDeath; //common problem: making sure this event doesnt get spammed
    [SerializeField] private int initialHealth = 30; //im scaling health x5 incase we need to balance 
    
    [Header("Debug Health")]
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool invincibleMode = false;
    [field: SerializeField] public int CurrentHealth { get; private set; }
    public virtual void Start()
    {
        CurrentHealth = initialHealth;
    }


    public abstract void HandleDeath(); 
    
    public void Heal(int healthAmt)
    {
        CurrentHealth += healthAmt;
        if (CurrentHealth > initialHealth)
            CurrentHealth = initialHealth;
        OnHealthChange?.Invoke(true);
    }

    public void Damage(int damageAmt)
    {
        if(invincibleMode)
            return;
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmt, 0);
        
        if (CurrentHealth == 0 && !isDead)
        {
                OnDeath?.Invoke();
                HandleDeath();
                isDead = true;
                return;
        }
        OnHealthChange?.Invoke(false);
        
    }
  

}