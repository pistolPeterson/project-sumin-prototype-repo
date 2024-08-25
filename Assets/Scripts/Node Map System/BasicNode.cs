using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNode : MonoBehaviour, INode
{
   
    public void OnNodeInteract()
    {
        //verify data for next encounter
       //go to encounter scene 
       Debug.Log("Going to next encounter!");
    }
}
