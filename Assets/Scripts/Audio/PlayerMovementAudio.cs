using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAudio : BaseAudio
{
    [SerializeField] private PlayerMovement playerMovement;

    public override void Start()
    {
        base.Start();
        playerMovement.OnPlayerMove.AddListener(PlayAudio);
    }
}
