using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> 
{
    [field: SerializeField] public GameObject playerObject { get; private set; }
    [field: SerializeField] public BossAttackHandler BossAttackHandler { get;  set; }
    private bool showLogs = true;
    public List<CardDataBaseSO> currentPlayerHand;
    
    
    [Header("Debug cards")] 
    public List<CardDataBaseSO> testingCardEffects;

    //PLAYER DATA
    private int HEAL_AMOUNT = 5;
    public bool willHealThisRound { get; set; } = false;
    public int CurrentHealth { get; set; } = 50;


    //NODE MAP DATA 
    public List<NodeEnum> MapNodeEnums;
    public int CurrentProgress { get; set; } = 0; 
    protected override void Awake()
    { 
      base.Awake();
    }

    public void Init()
    {
        MapNodeEnums = new List<NodeEnum>();

        MapNodeEnums = SaveManager.Instance.CurrentSave.mapNodeEnums;
        CurrentProgress = SaveManager.Instance.CurrentSave.currentNodeId;
        CurrentHealth = SaveManager.Instance.CurrentSave.health;
        currentPlayerHand = SaveManager.Instance.CurrentSave.playerCards;
    }
    public void OnStartEncounter()
    {
        if (!playerObject)
        {
            Debug.Log("Player GameObject not assigned in GameManager. Attempting to find in scene.");
            playerObject = FindObjectOfType<PlayerHealth>()?.gameObject;
        }

        playerObject.GetComponent<PlayerHealth>()?.LoadHealth();
        if (playerObject != null && willHealThisRound)
        {
            Debug.Log("Healed the player");
            playerObject.GetComponent<PlayerHealth>().Heal(HEAL_AMOUNT);

        }
        
        if (testingCardEffects.Count != 0)
        {
            currentPlayerHand = testingCardEffects;
            DebugReadCards(currentPlayerHand);
        }
        else
        {
            ReadCards();
        }
    }

    public void DebugReadCards(List<CardDataBaseSO> listOfCardData) //Reads through the cards and applies their BS
    {
        foreach (var cardData in listOfCardData)
        {
            cardData.CardEffect(this);
            String cardOrCurse = cardData.GetType().ToString();
            Log($"{cardOrCurse} Card applied: {cardData.cardDescription}");
        }
    }

     public void ReadCards() //Reads through the cards and applies their BS
    {
        foreach (var cardData in currentPlayerHand)
        {
            cardData.CardEffect(this);
            String cardOrCurse = cardData.GetType().ToString();
            Log($"{cardOrCurse} Card applied: {cardData.cardDescription}");
        }
    }


    public void OnEndEncounter()
    {
        willHealThisRound = false;
        
    }

    public void ResetGameManager()
    {
       // MapNodeEnums = new List<NodeEnum>();
       SaveManager.Instance.CreateNewSave();
       SaveManager.Instance.SaveAllDataOnline();
    }
    
    
    private void Log(object message)
    {
        if(showLogs)
            Debug.Log(message);
    }
}
