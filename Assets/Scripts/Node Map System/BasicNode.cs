using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicNode : INode
{
   
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        //TODO: verify data for next encounter
        Debug.Log("[Basic Node]: Going to boss scene");
        nodeMap.IncreaseProgress();
       SceneManager.LoadScene("MainEncounter");
    }
}
