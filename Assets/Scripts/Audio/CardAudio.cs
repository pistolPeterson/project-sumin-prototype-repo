using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardAudio : BaseAudio
{
   [SerializeField] private CardSelectHandler cardSelectHandler;
   public override void Start()
   {
      base.Start();
      cardSelectHandler.OnPlayerConfirmedCard.AddListener(PlayAudio);
      Debug.Log("started up card audio");
      
   }
}
