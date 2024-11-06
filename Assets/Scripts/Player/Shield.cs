using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Shield : MonoBehaviour {
    
    [SerializeField] private InputManager input;
     [SerializeField] private PlayerHealth playerHealth;

    public void EnableShield()
    {
        //logic 
        playerHealth.SetInvincibility(true);
        //visual
    }
    
    public void DisableShield()
    {
        //logic
        playerHealth.SetInvincibility(false);
        //visual
    }
}
