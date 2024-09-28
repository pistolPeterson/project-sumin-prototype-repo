using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class INode : MonoBehaviour
{
    protected NodeMap nodeMap; 
    public bool IsNodeActive { get; set; } = false;

    public abstract void OnNodeInteract();

    public virtual void Awake()
    {
        nodeMap = FindObjectOfType<NodeMap>();
    }
}
