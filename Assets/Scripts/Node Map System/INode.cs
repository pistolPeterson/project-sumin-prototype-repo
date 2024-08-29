using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode //refactor into abstract... whats stopping player from spam clicking nodes, only one node should be able to be clicked on. AND in order(later problem)
{
    public void OnNodeInteract();
}
