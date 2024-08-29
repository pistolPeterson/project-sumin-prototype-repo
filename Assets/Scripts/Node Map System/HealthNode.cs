using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : MonoBehaviour, INode
{
    public void OnNodeInteract()
    {
        
        //pete intrusive thought: if player skips 3 of these they can get a second chance thingy 
        Debug.Log("we healing");
        GameManager.Instance.willHealThisRound = true;


    }
}
