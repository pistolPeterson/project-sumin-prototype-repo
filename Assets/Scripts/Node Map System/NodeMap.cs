using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NodeMap : MonoBehaviour
{

    [Header("Game Data")] 
    private int amountOfEncounters = 11; //amt of nodes player must play through, NOT the amt of nodes in game 
    [SerializeField] private float distanceBetweenNodes = 10f;
    [SerializeField] private int currentNodeProgress = 0;
    [Header("Setup Data")]
    [SerializeField] private GameObject healNodePrefab;
    [SerializeField] private GameObject encounterNodePrefab;
    [SerializeField] private GameObject tarotCardNodePrefab;
    [SerializeField] private GameObject bossStatIncPrefab;
    [SerializeField] private Transform startingNodeLocation;
    public List<GameObject> currentNodesList;
    
    private void Start()
    {
        SpawnNodeMap();
        SetLineVisual();
    }

    private void SetLineVisual()
    {
        for (int i = 0; i < currentNodesList.Count - 1; i++)
        {
            GameObject currentNode = currentNodesList[i];
            LineRenderer lineRend = currentNode.GetComponentInChildren<LineRenderer>();
            lineRend.positionCount = 2;
            lineRend.SetPosition(0, currentNode.transform.position);
            
            lineRend.SetPosition(1, currentNodesList[i+1].transform.position);
        }
    }
    


    private void SpawnNodeMap()
    {
        currentNodesList = new List<GameObject>();
        var initialNode = Instantiate(encounterNodePrefab, startingNodeLocation.position, quaternion.identity); //spawn the starting encounter node
        currentNodesList.Add(initialNode);
        
        //spawn random nodes to the right
        for (int i = 0; i < amountOfEncounters; i++)
        {
            //choose the node 
            GameObject nodeToSpawn = GetRandomNode();
            //set the location 
            Vector3 locationToSpawn = new Vector3(startingNodeLocation.position.x + ((i + 1) * distanceBetweenNodes),
                startingNodeLocation.position.y, 0);
           
            //spawn
           var gameNode = Instantiate(nodeToSpawn, locationToSpawn, Quaternion.identity);
           currentNodesList.Add(gameNode);
        }

        Vector3 lastNodeLocation = new Vector3(startingNodeLocation.position.x + ((amountOfEncounters + 1) * distanceBetweenNodes),
            startingNodeLocation.position.y, 0);
       var finalNode = Instantiate(encounterNodePrefab, lastNodeLocation, quaternion.identity); //spawn the last encounter node
       currentNodesList.Add(finalNode);
    }

    [ProButton]
    private void UpdateNodeProgress() //warninng: doing logic and viusal in same method
    {
        foreach (var currentNode in currentNodesList)
        {
            currentNode.GetComponentInChildren<NodeVisual>().ShowNodeVisualActive(false);
            currentNode.GetComponent<INode>().IsNodeActive = false;
        }

        currentNodesList[currentNodeProgress].GetComponentInChildren<NodeVisual>().ShowNodeVisualActive(true);
        currentNodesList[currentNodeProgress].GetComponent<INode>().IsNodeActive = true;
    }
    private GameObject GetRandomNode()
    {
        int randomNum = Random.Range(0, 4);
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
}
