using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SpecialMove : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnCompleteSpecial;
    public virtual void Start() {

    }
    public void StartAttack() {

    }
    protected abstract void Attack();

}
