using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Start()
    {
        base.Start();
    }

 
    public override void HandleDeath()
    {
        Debug.Log("Player is dead af");
    }
}