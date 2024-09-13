using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAudio : BaseAudio
{
    [SerializeField] private PlayerHealth health;

    public override void Start()
    {
        base.Start();
        health.OnHealthChange.AddListener(PlayAudio);
    }

    private void PlayAudio(int healthAmount)
    {
       if(healthAmount < 0)
           PlayAudio();
    }
}
