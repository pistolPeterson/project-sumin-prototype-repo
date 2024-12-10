using System.Collections.Generic;

[System.Serializable]
public class SaveFile
{
    public List<NodeEnum> mapNodeEnums = new List<NodeEnum>();

    public int nodeCount;
    public int currentNodeId;
    public int health;

    public override string ToString()
    {
        var nodeEnumListStr = $"---Map Node Enums (Count:{mapNodeEnums.Count})---\n";

        for (var i = 0; i < mapNodeEnums.Count; i++){
            nodeEnumListStr += $"Node {i}:" + mapNodeEnums[i] + "\n";
        }

        return nodeEnumListStr + $"\nnodeCount:{nodeCount}\ncurrentNodeId:{currentNodeId}";
    }
}
