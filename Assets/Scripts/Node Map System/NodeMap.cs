using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class NodeMap : MonoBehaviour
{
    [Header("Game Data")]
    private int amountOfEncounters = 11; //amt of nodes player must play through, NOT the amt of nodes in game 

    [SerializeField] private float distanceBetweenNodes = 10f;
    private int currentNodeProgress = 0;
    public List<GameObject> currentNodesList;
    [SerializeField] private Transform startingNodeLocation;

    [Header("Prefabs")] 
    [SerializeField] private GameObject healNodePrefab;
    [SerializeField] private GameObject encounterNodePrefab;
    [SerializeField] private GameObject tarotCardNodePrefab;
    [SerializeField] private GameObject bossStatIncPrefab;

    //TODO: only use one event
   [HideInInspector] public UnityEvent<int, int> OnProgressUpdated;
   [HideInInspector] public UnityEvent OnNodeProgressUpdated;
    private void Start()
    {
        SetupNodeMap();
    }

//look for gamemanager, if map hasnt been generated, then generate map and send it back to gamemanager 
    //else read the node enums and generate the current map layout 
    //set the node progress as well
    private void SetupNodeMap()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("No GameManager in Scene.");
            return;
        }

        if (GameManager.Instance.MapNodeEnums.Count == 0)
        {
            GenerateNewNodeMap();
            GameManager.Instance.MapNodeEnums = ConvertNodeObjectsIntoNodeEnum();
        }
        else
        {
            Debug.Log("Loading Node Map From Game Manager");
            GenerateUserNodeMap(GameManager.Instance.MapNodeEnums);
            currentNodeProgress = GameManager.Instance.CurrentProgress;
        }
        UpdateNodeProgress();

    }

    private void SetLineVisual()
    {
        for (int i = 0; i < currentNodesList.Count - 1; i++)
        {
            GameObject currentNode = currentNodesList[i];
            LineRenderer lineRend = currentNode.GetComponentInChildren<LineRenderer>();
            lineRend.positionCount = 2;
            lineRend.SetPosition(0, currentNode.transform.position);

            lineRend.SetPosition(1, currentNodesList[i + 1].transform.position);
        }
    }


    public void IncreaseProgress()
    {
        currentNodeProgress++;
        GameManager.Instance.CurrentProgress = currentNodeProgress;
        UpdateNodeProgress();
    }

    private void GenerateUserNodeMap(List<NodeEnum> nodeEnums)
    {
        for (int i = 0; i < nodeEnums.Count; i++)
        {
            Vector3 locationToSpawn = SetNodePosition(i);
            var nodeGO = Instantiate(GetNodeFromEnum(nodeEnums[i]), locationToSpawn, quaternion.identity);
            currentNodesList.Add(nodeGO);
        }
        SetLineVisual();
    }

    [ProButton]
    private void GenerateNewNodeMap()
    {
        currentNodesList = new List<GameObject>();
        var initialNode =
            Instantiate(encounterNodePrefab, startingNodeLocation.position,
                quaternion.identity); //spawn the starting encounter node
        currentNodesList.Add(initialNode);

        //spawn random nodes to the right
        for (int i = 0; i < amountOfEncounters; i++)
        {
            //choose the node 
            GameObject nodeToSpawn = GetRandomNode();
            //set the location 
            Vector3 locationToSpawn = SetNodePosition(i);

            //spawn
            var gameNode = Instantiate(nodeToSpawn, locationToSpawn, Quaternion.identity);
            currentNodesList.Add(gameNode);
        }

        Vector3 lastNodeLocation = SetNodePosition(amountOfEncounters);
        var finalNode =
            Instantiate(encounterNodePrefab, lastNodeLocation, quaternion.identity); //spawn the last encounter node
        currentNodesList.Add(finalNode);

        SetLineVisual();
    }

    private Vector3 SetNodePosition(int index)
    {
        return new Vector3(startingNodeLocation.position.x + ((index + 1) * distanceBetweenNodes),
            startingNodeLocation.position.y, 0);
    }

    public GameObject GetCurrentNode()
    {
        return currentNodesList[currentNodeProgress];
    }
    
    [ProButton]
    private void UpdateNodeProgress() //TODO: warninng doing logic and viusal in same method
    {
        for (int i = 0; i < currentNodesList.Count; i++)
        {
          
            var nodeVisual = currentNodesList[i].GetComponentInChildren<NodeVisual>();
            var nodeComponent = currentNodesList[i].GetComponent<INode>();

            bool isActive = (i == currentNodeProgress);
            nodeVisual.ShowNodeVisualActive(isActive);
            nodeComponent.IsNodeActive = isActive;
        }
        OnProgressUpdated?.Invoke(currentNodeProgress, currentNodesList.Count);
        OnNodeProgressUpdated?.Invoke();
    }

    private GameObject GetRandomNode()
    {
        int nodeTypeCount = 4;
        int randomNum = Random.Range(0, nodeTypeCount);
        switch (randomNum)
        {
            case 0:
                return tarotCardNodePrefab;
            case 1:
                return encounterNodePrefab;
            case 2:
                return bossStatIncPrefab;
            case 3:
                return healNodePrefab;
        }

        return encounterNodePrefab;
    }

    private GameObject GetNodeFromEnum(NodeEnum nodeEnum)
    {
        switch (nodeEnum)
        {
            case NodeEnum.NONE:
                return null;
            case NodeEnum.TAROT:
                return tarotCardNodePrefab;
            case NodeEnum.BOSS_STAT:
                return bossStatIncPrefab;
            case NodeEnum.HEAL:
                return healNodePrefab;
            case NodeEnum.NORMAL:
                return encounterNodePrefab;
            default:
                throw new ArgumentOutOfRangeException(nameof(nodeEnum), nodeEnum, null);
        }
    }

    private List<NodeEnum> ConvertNodeObjectsIntoNodeEnum()
    {
      
        List<NodeEnum> nodeEnums = new List<NodeEnum>();
        foreach (var nodeGameObject in currentNodesList)
        {
            var nodeType = nodeGameObject.GetComponent<INode>();

            switch (nodeType)
            {
                case HealthNode:
                    nodeEnums.Add(NodeEnum.HEAL);
                    break;
                case BasicNode:
                    nodeEnums.Add(NodeEnum.NORMAL);
                    break;
                case BossNode:
                    nodeEnums.Add(NodeEnum.BOSS_STAT);
                    break;
                case TarotNode:
                    nodeEnums.Add(NodeEnum.TAROT);
                    break;
            }
        }
        return nodeEnums;
    }
}