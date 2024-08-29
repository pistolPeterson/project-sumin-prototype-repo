using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TarotNode : MonoBehaviour, INode
{
    public void OnNodeInteract()
    {
        Debug.Log("tarot card system! ");
        SceneManager.LoadScene(1);
    }
}
