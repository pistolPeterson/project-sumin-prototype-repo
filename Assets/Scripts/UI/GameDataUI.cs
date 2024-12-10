
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText; 
    [SerializeField] private TextMeshProUGUI currentCardsDisplayText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private NodeMap nodeMap;

    private void Awake()
    {
        nodeMap.OnProgressUpdated.AddListener(UpdateProgressText);
        progressText.text = "---";
    }

    private void Start()
    {
        UpdatePlayerCurrentCardsText();
    }

    private void UpdateProgressText(int currentProgress, int maxProgress)
    {
        progressText.text = $"Night {currentProgress+1} of {maxProgress} ";
    }


    private void UpdatePlayerCurrentCardsText()
    {
        List<CardDataBaseSO> currentCards = GameManager.Instance.currentPlayerHand;
        string text = "Current Cards Available\n";
        
        foreach (var card in currentCards)
        {
            text += card.cardName + "\n";
        }

        currentCardsDisplayText.text = text;
    }

    public void LoadHealthData( )
    {
        healthText.text = $"Health: {SaveManager.Instance.CurrentSave.health}";
    }

    public void SaveData(ref GameData data)
    {
       
    }
}
