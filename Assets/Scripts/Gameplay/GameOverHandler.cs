
using System;
using com.cyborgAssets.inspectorButtonPro;
using MaskTransitions;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private BossAttackHandler bossAttackHandler;
    private float newGameTransitionTime = 2.5f;
    private void Awake()
    {
        playerHealth.OnDeath.AddListener(HandleOnPlayerDeath);
    }

   
        [ProButton]
        public void HandleOnPlayerDeath()
        {
            bossAttackHandler.encounterTimer = bossAttackHandler.GetEncounterDuration();
            //if player is dead reset stuff 
            GameManager.Instance.ResetGameManager();
           
            TransitionManager.Instance.LoadLevel("MainMenu", newGameTransitionTime );
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
            SaveManager.Instance.SaveAllDataOnline();
            //go back to node map
            TransitionManager.Instance.LoadLevel("Scenes/Gameplay Scenes/RealNodeMap", newGameTransitionTime);
        }
        
   
    
}
