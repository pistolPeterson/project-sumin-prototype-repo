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
    
    
    //NODE MAP DATA 
    public List<NodeEnum> MapNodeEnums;
    public int CurrentProgress { get; set; } = 0; 
    protected override void Awake()
    {
        base.Awake();
        MapNodeEnums = new List<NodeEnum>();
    }


    public void OnStartEncounter()
    {
        if (!playerObject)
        {
            Debug.LogError("Player GameObject not assigned in GameManager. Attempting to find in scene.");
            playerObject = FindObjectOfType<PlayerHealth>()?.gameObject;
        }
        
        if (testingCardEffects.Count != 0)
        {
            currentPlayerHand = testingCardEffects;
            ReadCards(currentPlayerHand);
        }

        if (playerObject != null && willHealThisRound)
        {
            Debug.Log("Healed the player");
            playerObject.GetComponent<PlayerHealth>().Heal(HEAL_AMOUNT);

        }
        
        ReadCards();
    }

    public void ReadCards(List<CardDataBaseSO> listOfCardData) //Reads through the cards and applies their BS
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
     
    private void Log(object message)
    {
        if(showLogs)
            Debug.Log(message);
    }
}
