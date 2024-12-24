using System.Collections;
using System.Collections.Generic;
using PeteUnityUtils;
using UnityEngine;

public class AllCards : PersistentSingleton<AllCards>
{
    public List<CardDataBaseSO> CardPool;


    public Dictionary<CardMappingEnum, CardDataBaseSO> GenerateDictionary()
    {
        var dict = new Dictionary<CardMappingEnum, CardDataBaseSO>();
        foreach (var card in CardPool)
        {
            dict.Add(card.GetCardMappingEnum, card);
        }

        return dict;
    }

    public void ShuffleCardPool()
    {
        CardPool.Shuffle();
    }
}