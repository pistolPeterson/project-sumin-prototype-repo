using System;
using com.cyborgAssets.inspectorButtonPro;
using MaskTransitions;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private AmbienceSystem ambienceSystem;
    [SerializeField] private BossAttackHandler bossAttackHandler;
    private float newGameTransitionTime = 2.5f;

    private void Awake()
    {
        playerHealth.OnDeath.AddListener(HandleOnPlayerDeath);
    }

    // var clip = AudioUI.ClipsMainMenu.NewGameEvent -> one prefab with list of events in a scriptable prefab public events? 
    // also has PlayOneShot, and tells you how long it will last, a singleton, but doesnt move between scenes 
    //if null, console log error (give 1 sec as time?)
    //AudioUI.Play(AudioUI.MainMenuEvent); 


    [ProButton]
    public void HandleOnPlayerDeath()
    {
        PlayerDeathUISound();
        ambienceSystem.StopAmbienceSystem();
        bossAttackHandler.encounterTimer = bossAttackHandler.GetEncounterDuration();
        //if player is dead reset stuff 
        GameManager.Instance.ResetGameManager();

        TransitionManager.Instance.LoadLevel("MainMenu", newGameTransitionTime);
    }

    private void PlayerDeathUISound()
    {
        var audioClip = AudioSOHandler.Instance.EncounterAudioSO.PlayerDead;
        AudioSOHandler.Instance.PlayOneShot(audioClip);
    }

    public void EndRound()
    {
        ambienceSystem.StopAmbienceSystem();
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