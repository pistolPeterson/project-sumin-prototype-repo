using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float playFieldPosConstraint = 8f;
    [SerializeField] private float attackDuration = 5f; // how long an attack lasts for
    [SerializeField] protected float delayBetweenAttacks = 0.5f; // delay time before calling the next attack
    protected Vector3 projectileSpawnLoc;
    protected bool attackComplete = false;
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
    [ProButton]
    private void StopAttack() {
        StopCoroutine(AttackLoop());
        timer = attackDuration;
    }
    protected int GetRandomYPos() {
        int randomPosY = (int)UnityEngine.Random.Range(-playFieldPosConstraint, playFieldPosConstraint);
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
    }
    private bool IsAttackComplete() {
        return attackComplete;
    }
}
