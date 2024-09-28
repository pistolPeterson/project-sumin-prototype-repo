using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TarotNode :  INode
{
    public override void OnNodeInteract()
    {
        if(!IsNodeActive)
            return;
        Debug.Log("tarot card system! ");
        nodeMap.IncreaseProgress();
        SceneManager.LoadScene(1);
    }
}
