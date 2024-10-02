using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : INode
{
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        
        //pete intrusive thought: if player skips 3 of these they can get a second chance thingy 
        Debug.Log("we healing");
        nodeMap.IncreaseProgress();
        GameManager.Instance.willHealThisRound = true;


    }
}
