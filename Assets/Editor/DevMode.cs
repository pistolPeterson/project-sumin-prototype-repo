using UnityEngine;

using UnityEditor;

public class DevMode 
{
    [MenuItem("Dev Mode/Raeus Zoom")]
    public static void SkipToEncounterEnd()
    {
        Debug.Log("Attempting To Skip to End...");
        var bossAttackHandler = GameObject.FindObjectOfType<BossAttackHandler>();
        if (!bossAttackHandler)
        {
            Debug.LogError("Couldnt Find Boss Attack Handler in scene.");
            return;
        }
        
        bossAttackHandler.DebugNukeEncounterTime();
    }
    
    
    
    [MenuItem("Dev Mode/Toggle Goose Mode")]
    public static void ToggleInvincibiltyMode()
    {
        var playerHealth = GameObject.FindObjectOfType<Health>();
        if(!playerHealth)
            Debug.LogError("Couldnt Find player in scene.");
        var currentState = playerHealth.DebugInvincibilityMode;
        currentState = !currentState;
        playerHealth.DebugInvincibilityMode = currentState;
        if (currentState)
            Debug.Log("Goose Mode Activated");
        else
        {
            Debug.Log("Goose Mode Disabled");
        }
    }
    
    
    [MenuItem("Dev Mode/Boost Health")]
    public static void PlayerHealthBoost()
    {
        var playerHealth = GameObject.FindObjectOfType<Health>();
        if(!playerHealth)
            Debug.LogError("Couldnt Find player in scene.");
        playerHealth.Heal(25);
        Debug.Log("Gave Player some Adderall...");

    }
    
    [MenuItem("Dev Mode/Reset All Data")]
    public static void ResetData()
    {
       //DataPersistenceManager.Instance.NewGame();

    }
}
