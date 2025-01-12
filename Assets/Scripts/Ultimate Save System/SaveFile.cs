using System.Collections.Generic;

[System.Serializable]
public class SaveFile
{
    public List<NodeEnum> mapNodeEnums = new List<NodeEnum>();
    public static string MAP_NODE_ENUMS_KEY = "mapNodeEnums";
    
    public int nodeCount;
    public static string NODE_COUNT_KEY = "nodeCount";
    
    public int currentNodeId;
    public int health = 50;
    public static string CURRENT_NODE_ID_KEY = "currentNodeId";
    
   
    public static string HEALTH_KEY = "health";
    
    public List<CardDataBaseSO> playerCards = new List<CardDataBaseSO>();
    public static string PLAYER_CARDS_KEY = "playerCards";


    public bool SaveIsModified { get; set; } = false; 
    
    public Dictionary<string, object> ConvertDataToDictionary()
    {
        SaveIsModified = true;
        var data = new Dictionary<string, object>
        {
            { MAP_NODE_ENUMS_KEY, mapNodeEnums },
            { NODE_COUNT_KEY, nodeCount },
            { CURRENT_NODE_ID_KEY, currentNodeId },
            { HEALTH_KEY, health },
            { PLAYER_CARDS_KEY, ConvertPlayerCardsToEnum() }
        };

        return data;

    }

    private List<CardMappingEnum> ConvertPlayerCardsToEnum()
    {
        var mappingEnums = new List<CardMappingEnum>();
        foreach (var card in playerCards)
        {
            mappingEnums.Add(card.GetCardMappingEnum);
        }

        return mappingEnums;
    }

    public List<CardDataBaseSO> ConvertEnumToPlayerCards(List<CardMappingEnum> cardMapping)
    {
        var generatedPlayerCards = new List<CardDataBaseSO>();
        foreach (var cardTypeEnum in cardMapping)
        {
            generatedPlayerCards.Add(AllCards.Instance.GenerateDictionary()[cardTypeEnum]);
        }
        return generatedPlayerCards;
    }
    
    
    public override string ToString()
    {
        var nodeEnumListStr = $"---Map Node Enums (Count:{mapNodeEnums.Count})---\n";

        for (var i = 0; i < mapNodeEnums.Count; i++){
            nodeEnumListStr += $"Node {i}:" + mapNodeEnums[i] + "\n";
        }

        return nodeEnumListStr + $"\nnodeCount:{nodeCount}\ncurrentNodeId:{currentNodeId}";
    }

  
}
