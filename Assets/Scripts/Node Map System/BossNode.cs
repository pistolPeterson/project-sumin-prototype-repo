using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNode : MonoBehaviour, INode
{

    public void OnNodeInteract()
    {
        //increase boss stats 
        //show player somehow?
        Debug.Log("Increasing Boss stats... oh boy");
    }
}
