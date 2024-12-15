
using System;
using com.cyborgAssets.inspectorButtonPro;
using MaskTransitions;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private BossAttackHandler bossAttackHandler;
    private float transitionToNewGameTime = 2.5f;
    private void Awake()
    {
        playerHealth.OnDeath.AddListener(HandleOnPlayerDeath);
    }

    //player death handler
        //encounter bar stops
        //boss stops attack
        //reset save data (also save other relevant stats, like new cards)
        //go to node map scene, after certain amount of seconds 
        [ProButton]
        public void HandleOnPlayerDeath()
        {
            bossAttackHandler.encounterTimer = bossAttackHandler.GetEncounterDuration();
            //if player is dead reset stuff 
            GameManager.Instance.ResetGameManager();
           
            //else continue as normal
            TransitionManager.Instance.LoadLevel("Scenes/Gameplay Scenes/RealNodeMap", transitionToNewGameTime);

        }

        public void EndRound()
        {
            if (playerHealth.CurrentHealth == 0)
            {
                Debug.Log("player is dead we should be restarting scene");
                return;
            }
            playerHealth.SaveHealth();
            //boss stops attack 
            Debug.Log("didnt die we going back");
            //go back to node map
            TransitionManager.Instance.LoadLevel("Scenes/Gameplay Scenes/RealNodeMap", transitionToNewGameTime);
        }
        
        
        
    //player reaches end 
        //boss stops attack
        //go to node map scene
    
}
