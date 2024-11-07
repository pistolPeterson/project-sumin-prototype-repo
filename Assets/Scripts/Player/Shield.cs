using System;
using UnityEngine;


public class Shield : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ShieldVisual shieldVisual;


    private void Start()
    {
        input.OnShieldPressed.AddListener(EnableShield);
        input.OnShieldReleased.AddListener(DisableShield);
    }
    //TODO: player presses key, player is invincible for x seconds 
    //if player moves, early exit 
    //on exit/early exit theres a cooldown for x seconds

    public void EnableShield()
    {
        //logic 
        playerHealth.SetInvincibility(true);
        //visual
        shieldVisual.ShowShieldVisual();
    }

    public void DisableShield()
    {
        //logic
        playerHealth.SetInvincibility(false);
        //visual
        shieldVisual.CloseShieldVisual();
    }
}