using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class INode : MonoBehaviour //refactor into abstract... whats stopping player from spam clicking nodes, only one node should be able to be clicked on. AND in order(later problem)
{
    public bool IsNodeActive { get; set; } = false;

    public abstract void OnNodeInteract();
}
