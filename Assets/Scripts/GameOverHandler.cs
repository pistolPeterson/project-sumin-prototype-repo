
using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private BossAttackHandler bossAttackHandler;
    private void Awake()
    {
        playerHealth.OnDeath.AddListener(HandleOnPlayerDeath);
    }

    //player death handler
        //encounter bar stops
        //boss stops attack
        //reset (also save other relevant stats, like new cards)
        //go to node map scene, after certain amount of seconds 
        [ProButton]
        public void HandleOnPlayerDeath()
        {
            bossAttackHandler.encounterTimer = bossAttackHandler.GetEncounterDuration();
        }
    
        
        
        
        
    //player reaches end 
        //boss stops attack
        //go to node map scene
}
