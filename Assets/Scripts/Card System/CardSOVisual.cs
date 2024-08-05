using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//this script takes in the CardSO and sets it up on the card UI
public class CardSOVisual : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardSymbolImage;
    public List<Sprite> cardBackground; //0 is good, 1 is bad
    [field: SerializeField] public CardDataBaseSO cardSo;
    [field: SerializeField] public Sprite TarotSymbolSprite { get; set; }
    [field: SerializeField] public TextMeshProUGUI CardName { get; set; }
    [field: SerializeField] public TextMeshProUGUI CardDescription { get; set; }


    private void Start()
    {
        UpdateCardVisual();
    }

    [ProButton]
    public void UpdateCardVisual()
    {
        if (cardSo == null)
        {
            Debug.LogError("The card SO is null. you broke it raeus.");
            return;
        }

        cardImage.sprite = cardSo is BlessCardBase ? cardBackground[0] : cardBackground[1];
        cardSymbolImage.sprite = cardSo.cardSprite;
        CardName.text = cardSo.cardName;
        CardDescription.text = cardSo.cardDescription;

    }
}
