using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public GameObject playerObject { get; private set; }


    private void Awake()
    {
        if (!playerObject)
        {
            Debug.LogError("player gameobject not assigned in gamemanager. attempting to find in scene");
            playerObject = FindObjectOfType<PlayerHeatlh>().gameObject;
            if (playerObject)
                Debug.Log("found player GO, youre lucky for now. punk.");
        }
    }

    //dont touch raeus
}
