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
       
        nodeMap.IncreaseProgress();
        SceneManager.LoadScene("CardSystemScene");
    }
}
