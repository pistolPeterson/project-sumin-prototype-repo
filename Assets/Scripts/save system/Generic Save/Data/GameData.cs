
using System;
using UnityEngine;
[Serializable]
public class GameData
{
  

   public int deathCount;
   public int currentHealth; 
   public float sfxVolume;
   public float musicVolume;
   
   public GameData()
   {
      this.deathCount = 0;
      this.currentHealth = 50;
      this.sfxVolume = 100;
      this.musicVolume = 100;
   }

  
   
}
