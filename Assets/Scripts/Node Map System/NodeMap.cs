using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NodeMap : MonoBehaviour
{

    [Header("Game Data")] 
    [SerializeField] private int amountOfEncounters = 11; //amt of encounters player must play through, NOT the amt of nodes in game 
    //Hi rae: I like the idea of the player having to play through 13 rounds. makes it feel spooky. 
    [SerializeField] private float distanceBetweenNodes = 10f;
    
    [Header("Setup Data")]
    [SerializeField] private GameObject healNodePrefab;
    [SerializeField] private GameObject encounterNodePrefab;
    [SerializeField] private GameObject tarotCardNodePrefab;
    [SerializeField] private GameObject bossStatIncPrefab;
    [SerializeField] private Transform startingNodeLocation;

    
    
    private void Start()
    {
        SpawnNodeMap();
    }

    private void SpawnNodeMap()
    {
        Instantiate(encounterNodePrefab, startingNodeLocation.position, quaternion.identity); //spawn the starting encounter node

        //spawn random nodes to the right
        for (int i = 0; i < amountOfEncounters; i++)
        {
            //choose the node 
            GameObject nodeToSpawn = GetRandomNode();
            //set the location 
            Vector3 locationToSpawn = new Vector3(startingNodeLocation.position.x + ((i + 1) * distanceBetweenNodes),
                startingNodeLocation.position.y, 0);
           
            //spawn
            Instantiate(nodeToSpawn, locationToSpawn, Quaternion.identity);
        }

        Vector3 lastNodeLocation = new Vector3(startingNodeLocation.position.x + ((amountOfEncounters + 1) * distanceBetweenNodes),
            startingNodeLocation.position.y, 0);
        Instantiate(encounterNodePrefab, lastNodeLocation, quaternion.identity); //spawn the last encounter node
    }


    private GameObject GetRandomNode()
    {
        int randomNum = Random.Range(0, 4);
        switch (randomNum)
        {
            case 0:
                return tarotCardNodePrefab;
                break;
            case 1:
                return encounterNodePrefab;
                break;
            case 2:
                return bossStatIncPrefab;
                break;
            case 3:
                return healNodePrefab;
                break;
            default:
                Debug.LogError("ERROR GETTING RANDOM NODE");
                return encounterNodePrefab;
                break;
            
        }

    }
}
