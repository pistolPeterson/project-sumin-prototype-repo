using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackPattern : MonoBehaviour
{
    protected float playFieldPosConstraint = 8f;
    [HideInInspector] public UnityEvent OnCompleteAttack; // when the entire attack loop is complete
    [HideInInspector] public UnityEvent OnCompleteSpecial; // when the entire special loop is complete
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] private float attackDuration = 5f; // how long an attack lasts for
    [SerializeField] protected float delayBetweenAttacks = 0.5f; // delay time before calling the next attack
    protected Vector3 projectileSpawnLoc;
    protected bool attackComplete = false;
    protected bool attackStopped = false;
    [SerializeField] protected bool isSpecialMove = false;
    private float timer = 0f;

    protected virtual void Start() {
        projectileSpawnLoc = transform.position;        
    }
    protected abstract void Attack();
    protected void SetSpawnLoc(float yPos) {
        projectileSpawnLoc = new Vector3(transform.position.x, yPos, transform.position.z);
    }
    public void StartAttack() {
       
        StartCoroutine(AttackLoop());
    }
   
    public void StopAttack() {
        timer = attackDuration;
        attackStopped = true;
        Debug.Log("Attack stopped");
    }
    protected int GetRandomYPos() {
        int randomPosY = (int)UnityEngine.Random.Range(-playFieldPosConstraint - 1, playFieldPosConstraint + 1); // +-1 to include constraint
        if (randomPosY % 2 != 0) {
            randomPosY++;
        }
        return randomPosY;
    }
    
    private IEnumerator AttackLoop() {
        timer = 0f;
      
        while (timer < attackDuration) {
           // Debug.Log("Attacking Time remaining: " + (attackDuration - timer));
            Attack();
            yield return new WaitUntil(IsAttackComplete);
          //  Debug.Break();
            yield return new WaitForSeconds(delayBetweenAttacks);
            timer += Time.fixedDeltaTime + delayBetweenAttacks; // to normalize the time
        }
        yield return new WaitForSeconds(delayBetweenAttacks);
        if (!attackStopped)
            OnCompleteAttack.Invoke();
        if (isSpecialMove) OnCompleteSpecial.Invoke();
    }
    private bool IsAttackComplete() {
        return attackComplete;
    }
    public float GetAttackDuration() {
        return attackDuration;
    }
}
