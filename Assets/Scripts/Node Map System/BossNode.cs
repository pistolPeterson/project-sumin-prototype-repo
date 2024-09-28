using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNode : INode
{

    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        //increase boss stats 
        //show player somehow in UI
        nodeMap.IncreaseProgress();
        Debug.Log("Increasing Boss stats... thats a raeus problem :) ");
    }
}
