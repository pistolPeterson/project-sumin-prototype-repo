using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//dont touch raeus :)
{
    [field: SerializeField] public GameObject playerObject { get; private set; }
    [field: SerializeField] public BossAttackHandler BossAttackHandler { get; private set; }
    private bool showLogs = true;
    public List<CardDataBaseSO> currentPlayerHand;
    
    
    [Header("Debug cards")] 
    public List<CardDataBaseSO> testingCardEffects;
    private void Awake()
    {
        if (!playerObject)
        {
            Debug.LogError("player gameobject not assigned in gamemanager. attempting to find in scene");
            playerObject = FindObjectOfType<PlayerHealth>().gameObject;
            if (playerObject)
                Debug.Log("found player GO, youre lucky for now. punk.");
        }
    }

    private void Start()
    {
        if (testingCardEffects.Count != 0)
        {
            currentPlayerHand = testingCardEffects;
            ReadCards(currentPlayerHand);
        }
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
    
    private void Log(object message)
    {
        if(showLogs)
            Debug.Log(message);
    }
}
