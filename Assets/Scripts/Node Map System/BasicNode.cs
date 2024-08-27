using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicNode : MonoBehaviour, INode
{
   
    public void OnNodeInteract()
    {
        //TODO: verify data for next encounter
        Debug.Log("[Basic Node]: Going to boss scene");       
       SceneManager.LoadScene(2);
    }
}
